# SimpleTaskManager
Applet for creation and recording tasks of various projects.

Разработать приложение для регистрации выполняемых на проекте задач.
Приложение должно позволять вводить, редактировать и удалять следующие данные:
1.	Задача
* a.	Идентификатор 
* b.	Название 
* c.	Объем работы 
* d.	Дата начала 
* e.	Дата окончания 
* f.	Статус (Не начата | В процессе | Завершена | Отложена) 
* g.	Исполнитель 
2.	Персона (Исполнитель) 
* a.	Идентификатор 
* b.	Фамилия 
* c.	Имя 
* d.	Отчество 

Приложение должно содержать следующие основные элементы:
1.	Главное Меню 
* a.	Задачи: Отображается форма “Список задач” 
* b.	Персоны: Отображается форма “Список персон”
2.	Форма “Список задач”
* a.	Колонки: 
	* i.	Идентификатор
	* ii.	Название
	* iii.	Работа
	* iv.	Дата начала
	* v.	Дата окончания
	* vi.	Статус (Не начата | В процессе | Завершена | Отложена)
	* vii.	Исполнитель
* b.	Команды уровня формы 
* i.	Добавить: Отображается форма ввода задачи в режиме добавления
* c.	Команды уровня записи 
	* i.	Изменить: Отображается форма ввода задачи в режиме редактирования
	* ii.	Удалить: Текущая запись удаляется

3.	Форма “Список исполнителей” 
* a.	Колонки: 
	* i.	Идентификатор
	* ii.	Фамилия
	* iii.	Имя
	* iv.	Отчество
* b.	Команды уровня формы 
	* i.	Добавить: Отображается форма ввода персоны в режиме добавления
* c.	Команды уровня записи 
	* i.	Изменить: Отображается форма ввода персоны в режиме редактирования
	* ii.	Удалить: Текущая запись удаляется

4.	Форма ввода задачи 
* a.	Поля: 
	* i.	Идентификатор: порядковый номер задачи; формируется автоматически; недоступно для изменения
	* ii.	Название
	* iii.	Работа
	* iv.	Дата начала
	* v.	Дата окончания
	* vi.	Статус (Не начата | В процессе | Завершена | Отложена)
	* vii.	Исполнитель: выбирается из списка персон
* b.	Команды: 
	* i.	Сохранить: введенные данные сохраняются в базе; управление передается в форму “Список задач”
	* ii.	Отмена: управление передается в форму “Список задач ”

5.	Форма ввода персоны (исполнителя) 
* a.	Поля: 
	* i.	Идентификатор
	* ii.	Фамилия
	* iii.	Имя
	* iv.	Отчество
* b.	Команды: 
	* i.	Сохранить: введенные данные сохраняются в базе; управление передается в форму “Список персон”
	* ii.	Отмена: управление передается в форму “Список персон”

*Замечание по терминологии: “команда” обозначает любой элемент управления, используемый для запуска операции, к примеру, это может быть кнопка, пиктограмма, гиперлинк и т.п. 
