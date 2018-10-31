using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using TaskManager.Services;
using TaskManager.Data.Entities;


namespace TaskManager
{
    public class Global : System.Web.HttpApplication
    {
        #region Variables

        private static string _connectionString = ConfigurationManager.ConnectionStrings["TrainingTaskerConnectionString"].ConnectionString;
        private static PersonBlo persons = new PersonBlo(_connectionString);
        private static ProjectBlo projectBlo = new ProjectBlo(_connectionString);
        private static TaskBlo taskBlo = new TaskBlo(_connectionString);
        private static ExceptionHandler _exceptionHandler;

        #endregion Variables

        #region Properties

        public static PersonBlo PersonsBlo
        {
            get { return persons; }
        }

        public static ProjectBlo ProjectsBlo
        {
            get { return projectBlo; }
        }

        public static TaskBlo TasksBlo
        {
            get { return taskBlo; }
        }

        public static ExceptionHandler ExcepHandler
        {
            get
            {
                if (_exceptionHandler == null) RegisterHandlers();
                return _exceptionHandler;
            }
        }

        #endregion Properties

        #region Supporting methods

        private static void RegisterHandlers()
        {
            _exceptionHandler = new ExceptionHandler();
            _exceptionHandler.RegisterHandler<SqlException>(e => Loger.WriteLog("Server is unavailable: " + e.ToString()));
            _exceptionHandler.RegisterHandler<FormatException>(
                e => Loger.WriteLog("Input string was not in a correct format:" + e.ToString()));
            _exceptionHandler.RegisterDefaultHandler(
                e => Loger.WriteLog("Unhandled exceprion:" + e.ToString()));
        }

        #endregion Supporting methods

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exception = Server.GetLastError().GetBaseException();
            Session["LastException"] = exception;
            Server.Transfer("Error.aspx");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
