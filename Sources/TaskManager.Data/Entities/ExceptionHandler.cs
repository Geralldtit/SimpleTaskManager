using System;
using System.Collections.Generic;
using System.IO;

namespace TaskManager.Data.Entities
{
	#region ExceptionHandler

	#region ExceptionHandler interface

	public interface IExceptionHandler
	{
		T Handle<T>(Func<T> func);
		T Handle<T>(Func<T> func, Action finallyAction);
		void Handle(Action action);
		void Handle(Action action, Action finallyAction);

		T HandleError<T>(Func<T> func, Action<Exception> handler);
		T HandleError<T>(Func<T> func, Action<Exception> handler, Action finallyAction);
		void HandleError(Action action, Action<Exception> handler);
		void HandleError(Action action, Action<Exception> handler, Action finallyAction);

		T Process<T>(Func<T> func);
		T Process<T>(Func<T> func, Action finallyAction);
		void Process(Action action);
		void Process(Action action, Action finallyAction);

		T ProcessError<T>(Func<T> func, Func<Exception, T> handler);
		T ProcessError<T>(Func<T> func, Func<Exception, T> handler, Action finallyAction);
		void ProcessError(Action action, Action<Exception> handler);
		void ProcessError(Action action, Action<Exception> handler, Action finallyAction);

		bool HasError(Action action, Func<Exception, bool> rethrowHandler, Action finallyAction);
		bool HasError(Action action, Action finallyAction);
		bool HasError(Action action);
		bool HasError<T>(Func<T> func, Func<Exception, bool> rethrowHandler, Action finallyAction, out T operationResult);
		bool HasError<T>(Func<T> func, Action finallyAction, out T operationResult);
		bool HasError<T>(Func<T> func, out T operationResult);


		IExceptionHandler RegisterHandler<T>(Action<T> handler);
		IExceptionHandler RegisterDefaultHandler(Action<Exception> handler);
		IExceptionHandler UnregisterHandler<T>(Action<T> handler);
		IExceptionHandler UnregisterDefaultHandler(Action<Exception> handler);
	}

	#endregion ExceptionHandler interface

	#region ExceptionHandler class


	public class ExceptionHandler : IExceptionHandler
	{
		private readonly IList<Action<Exception>> _defaultHandlers = new List<Action<Exception>>();
		private readonly Dictionary<Type, IList<object>> _handlers = new Dictionary<Type, IList<object>>();

		#region IExceptionHandler Members

		public T Handle<T>(Func<T> func)
		{
			return Handle(func, null);
		}

		public void Handle(Action action)
		{
			Handle(action, null);
		}

		public T Process<T>(Func<T> func)
		{
			return Process(func, null);
		}

		public void Process(Action action)
		{
			Process(action, null);
		}

		public T Handle<T>(Func<T> func, Action finallyAction)
		{
			return HandleError(func, null, finallyAction);
		}

		public void Handle(Action action, Action finallyAction)
		{
			HandleError(action, null, finallyAction);
		}

		public T Process<T>(Func<T> func, Action finallyAction)
		{
			return ProcessError(func, null, finallyAction);
		}

		public void Process(Action action, Action finallyAction)
		{
			ProcessError(action, null, finallyAction);
		}

		public T ProcessError<T>(Func<T> func, Func<Exception, T> handler)
		{
			return ProcessError(func, handler, null);
		}

		public T ProcessError<T>(Func<T> func, Func<Exception, T> handler, Action finallyAction)
		{
			try
			{
				return func();
			}
			catch (Exception e)
			{
				CallExceptionHandler(e);
				return handler != null ? handler(e) : default(T);
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}
		}

		public void ProcessError(Action action, Action<Exception> handler)
		{
			ProcessError(action, handler, null);
		}

		public void ProcessError(Action action, Action<Exception> handler, Action finallyAction)
		{
			try
			{
				action();
			}
			catch (Exception e)
			{
				if (handler != null)
					handler(e);

				CallExceptionHandler(e);
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}
		}

		public T HandleError<T>(Func<T> func, Action<Exception> handler)
		{
			return HandleError(func, handler, null);
		}

		public T HandleError<T>(Func<T> func, Action<Exception> handler, Action finallyAction)
		{
			try
			{
				return func();
			}
			catch (Exception e)
			{
				if (handler != null)
					handler(e);

				CallExceptionHandler(e);
				throw;
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}
		}

		public void HandleError(Action action, Action<Exception> handler)
		{
			HandleError(action, handler, null);
		}

		public void HandleError(Action action, Action<Exception> handler, Action finallyAction)
		{
			try
			{
				action();
			}
			catch (Exception e)
			{
				if (handler != null)
					handler(e);

				CallExceptionHandler(e);
				throw;
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}
		}

		public bool HasError(Action action, Func<Exception, bool> rethrowHandler, Action finallyAction)
		{
			try
			{
				action();
			}
			catch (Exception exception)
			{
				CallExceptionHandler(exception);
				if (rethrowHandler != null && rethrowHandler(exception))
					throw;

				return true;
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}

			return false;
		}

		public bool HasError(Action action, Action finallyAction)
		{
			return HasError(action, null, finallyAction);
		}

		public bool HasError(Action action)
		{
			return HasError(action, null, null);
		}

		public bool HasError<T>(Func<T> func, Action finallyAction, out T operationResult)
		{
			return HasError(func, null, finallyAction, out operationResult);
		}

		public bool HasError<T>(Func<T> func, out T operationResult)
		{
			return HasError(func, null, null, out operationResult);
		}

		public bool HasError<T>(Func<T> func, Func<Exception, bool> rethrowHandler, Action finallyAction,
								out T operationResult)
		{
			try
			{
				operationResult = func();
			}
			catch (Exception exception)
			{
				CallExceptionHandler(exception);
				if (rethrowHandler != null && rethrowHandler(exception))
					throw;

				operationResult = default(T);
				return true;
			}
			finally
			{
				if (finallyAction != null)
					finallyAction();
			}
			return false;
		}

		public IExceptionHandler RegisterHandler<T>(Action<T> handler)
		{
			if (!_handlers.ContainsKey(typeof(T)))
				_handlers.Add(typeof(T), new List<object>());

			_handlers[typeof(T)].Add(handler);

			return this;
		}

		public IExceptionHandler RegisterDefaultHandler(Action<Exception> handler)
		{
			_defaultHandlers.Add(handler);
			return this;
		}

		public IExceptionHandler UnregisterHandler<T>(Action<T> handler)
		{
			_handlers[typeof(T)].Remove(handler);
			return this;
		}

		public IExceptionHandler UnregisterDefaultHandler(Action<Exception> handler)
		{
			_defaultHandlers.Remove(handler);
			return this;
		}

		#endregion

		private void CallExceptionHandler(Exception exception)
		{
			if (_handlers.ContainsKey(exception.GetType()))
			{
				foreach (var handler in _handlers[exception.GetType()])
				{
					var invokeMethod = handler.GetType().GetMethod("Invoke");

					var handlerObject = handler;
					Handle(() => invokeMethod.Invoke(handlerObject, new object[] { exception }));
				}
			}
			else
			{
				foreach (var handler in _defaultHandlers)
					handler(exception);
			}
		}
	}

	#endregion ExceptionHandler class

	#endregion ExceptionHandler

	#region Logger class

	/// <summary>
	/// Static class for logging messages
	/// </summary>
	public static class Loger
	{
		private static LogerTraining _logerTraining = new LogerTraining();

		/// <summary>
		/// Property for Log file name
		/// </summary>
		public static string LogFileName
		{
			get { return _logerTraining.FileName; }
			set
			{
				if (_logerTraining == null)
					_logerTraining = new LogerTraining(value);
				else
					_logerTraining.FileName = value;
			}
		}

		/// <summary>
		/// Property for wite into Log file
		/// </summary>
		/// <param name="message">string message for log file</param>
		public static void WriteLog(string message)
		{
			if (message != "")
				_logerTraining.WriteLog(message);
		}
	}

	#region Suppoting logger class

	/// <summary>
	/// Class for logging messages
	/// </summary>
	internal class LogerTraining
	{
		private const string DefaultFileName = "logFile.log";
		private string _fileName;

		#region Constructots

		public LogerTraining() : this(DefaultFileName) { }

		public LogerTraining(string fileName)
		{
			_fileName = fileName;
			if (!CreateLogFile()) _fileName = DefaultFileName;
		}

		#endregion Constructots

		#region Properties

		/// <summary>
		/// Property for file name
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
			set
			{
				_fileName = value;
				CreateLogFile();
			}
		}

		#endregion Properties

		/// <summary>
		/// Write message into LogFile
		/// </summary>
		/// <param name="message">message for logFile</param>
		public void WriteLog(string message)
		{
			using (StreamWriter writer = new StreamWriter(_fileName, true))
			{
				writer.WriteLine("{------------ SystemTime: " + DateTime.Now + " ------------}");
				writer.WriteLine();
				writer.WriteLine(" Message:     " + message + "");
				writer.WriteLine();
				writer.WriteLine("{------------ SystemTime: " + DateTime.Now + " ------------}");
				writer.WriteLine();
			}
		}

		#region Supporting methods

		/// <summary>
		/// Create log file if not exists
		/// </summary>
		/// <returns></returns>
		private bool CreateLogFile()
		{
			if (!System.IO.File.Exists(_fileName))
				using (FileStream stream = new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					return true;
				}
			return false;
		}

		#endregion Supporting methods

	}

	#endregion Suppoting logger class

	#endregion Logger class
}
