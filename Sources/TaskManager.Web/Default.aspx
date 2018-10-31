<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TaskManager.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    

<h1><a name="приложение_для_управления_задачами" id="приложение_для_управления_задачами">Приложение для управления задачами</a></h1>
<div class="level1">

<p>

Постановка задачи: разработать приложение для регистрации выполняемых на проекте задач.
</p>
</div>
<h2><a name="функциональные_требования" id="функциональные_требования">Функциональные требования</a></h2>
<div class="level2">

<p>

Приложение должно позволять создавать, редактировать и удалять следующие сущности:

</p>
<ol>
<li class="level1"><div class="li"> Проект</div>
<ul>
<li class="level2"><div class="li"> Название</div>
</li>
<li class="level2"><div class="li"> Сокращенное название</div>
</li>
<li class="level2"><div class="li"> Описание</div>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Задача</div>
<ul>
<li class="level2"><div class="li"> Название</div>
</li>
<li class="level2"><div class="li"> Работа (часы)</div>
</li>
<li class="level2"><div class="li"> Дата начала</div>
</li>
<li class="level2"><div class="li"> Дата окончания</div>
</li>
<li class="level2"><div class="li"> Статус (Не начата | В процессе | Завершена | Отложена)</div>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Сотрудник</div>
<ul>
<li class="level2"><div class="li"> Фамилия</div>
</li>
<li class="level2"><div class="li"> Имя</div>
</li>
<li class="level2"><div class="li"> Отчество</div>
</li>
<li class="level2"><div class="li"> Должность</div>
</li>
</ul>
</li>
</ol>

<p>

Между перечисленными выше объектами необходимо реализовать следующие связи:

</p>
<ul>
<li class="level1"><div class="li"> Проект может включать в себя от нуля до множества задач</div>
</li>
<li class="level1"><div class="li"> Один сотрудник может быть назначен на множество задач</div>
</li>
<li class="level1"><div class="li"> Дополнительное требование: Одна и та же задача может выполняться множеством сотрудников (см. <a href="#дополнительные_требования" title="training:sandbox:tasks:task_manager &crarr;" class="wikilink1">Дополнительные требования</a>) </div>
</li>
</ul>

<p>

Приложение должно содержать следующие основные элементы:

</p>
<ol>
<li class="level1"><div class="li"> Главное Меню</div>
<ul>
<li class="level2"><div class="li"> Проекты: Отображается форма “Список проектов”</div>
</li>
<li class="level2"><div class="li"> Задачи: Отображается форма “Список задач”</div>
</li>
<li class="level2"><div class="li"> Персоны: Отображается форма “Список сотрудников”</div>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма “Список проектов”</div>
<ul>
<li class="level2"><div class="li"> Колонки:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор</div>
</li>
<li class="level3"><div class="li"> Название</div>
</li>
<li class="level3"><div class="li"> Сокращенное название</div>
</li>
<li class="level3"><div class="li"> Описание</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня формы</div>
<ul>
<li class="level3"><div class="li"> Добавить: Отображается форма ввода проекта в режиме добавления</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня записи</div>
<ul>
<li class="level3"><div class="li"> Изменить: Отображается форма ввода проекта в режиме редактирования</div>
</li>
<li class="level3"><div class="li"> Удалить: Текущая запись удаляется</div>
</li>
</ul>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма “Список задач”</div>
<ul>
<li class="level2"><div class="li"> Колонки:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор</div>
</li>
<li class="level3"><div class="li"> Проект (Сокращенное название)</div>
</li>
<li class="level3"><div class="li"> Название</div>
</li>
<li class="level3"><div class="li"> Дата начала</div>
</li>
<li class="level3"><div class="li"> Дата окончания</div>
</li>
<li class="level3"><div class="li"> Исполнитель (ФИО)</div>
</li>
<li class="level3"><div class="li"> Статус (Не начата | В процессе | Завершена | Отложена)</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня формы</div>
<ul>
<li class="level3"><div class="li"> Добавить: Отображается форма ввода задачи в режиме добавления</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня записи</div>
<ul>
<li class="level3"><div class="li"> Изменить: Отображается форма ввода задачи в режиме редактирования</div>
</li>
<li class="level3"><div class="li"> Удалить: Текущая запись удаляется</div>
</li>
</ul>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма “Список сотрудников”</div>
<ul>
<li class="level2"><div class="li"> Колонки:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор</div>
</li>
<li class="level3"><div class="li"> Фамилия</div>
</li>
<li class="level3"><div class="li"> Имя</div>
</li>
<li class="level3"><div class="li"> Отчество</div>
</li>
<li class="level3"><div class="li"> Должность</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня формы</div>
<ul>
<li class="level3"><div class="li"> Добавить: Отображается форма ввода сотрудника в режиме добавления</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды уровня записи</div>
<ul>
<li class="level3"><div class="li"> Изменить: Отображается форма ввода сотрудника в режиме редактирования</div>
</li>
<li class="level3"><div class="li"> Удалить: Текущая запись удаляется</div>
</li>
</ul>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма ввода проекта</div>
<ul>
<li class="level2"><div class="li"> Поля:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор: порядковый номер проекта; формируется автоматически; недоступно для изменения</div>
</li>
<li class="level3"><div class="li"> Название</div>
</li>
<li class="level3"><div class="li"> Сокращенное название</div>
</li>
<li class="level3"><div class="li"> Описание</div>
</li>
<li class="level3"><div class="li"> Список задач, принадлежащих проекту:</div>
<ul>
<li class="level4"><div class="li"> Колонки:</div>
<ul>
<li class="level5"><div class="li"> Идентификатор</div>
</li>
<li class="level5"><div class="li"> Название</div>
</li>
<li class="level5"><div class="li"> Дата начала</div>
</li>
<li class="level5"><div class="li"> Дата окончания</div>
</li>
<li class="level5"><div class="li"> Исполнитель (ФИО)</div>
</li>
<li class="level5"><div class="li"> Статус (Не начата | В процессе | Завершена | Отложена)</div>
</li>
</ul>
</li>
<li class="level4"><div class="li"> Команды уровня формы</div>
<ul>
<li class="level5"><div class="li"> Добавить: Отображается форма ввода задачи в режиме добавления (поле ввода проекта установлено равным текущему проекту и недоступно для редактирования)</div>
</li>
</ul>
</li>
<li class="level4"><div class="li"> Команды уровня записи</div>
<ul>
<li class="level5"><div class="li"> Изменить: Отображается форма ввода задачи в режиме редактирования</div>
</li>
<li class="level5"><div class="li"> Удалить: Текущая запись удаляется</div>
</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды:</div>
<ul>
<li class="level3"><div class="li"> Сохранить: введенные данные сохраняются в базе; управление передается в форму “Список проектов”</div>
</li>
<li class="level3"><div class="li"> Отмена: управление передается в форму “Список проектов”</div>
</li>
</ul>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма ввода задачи</div>
<ul>
<li class="level2"><div class="li"> Поля:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор: порядковый номер задачи; формируется автоматически; недоступно для изменения</div>
</li>
<li class="level3"><div class="li"> Проект: выбирается из списка проектов; если форма открыта из списка задач формы ввода проектов, то данное поле установлено равным текущему проекту и недоступно для редактирования</div>
</li>
<li class="level3"><div class="li"> Название</div>
</li>
<li class="level3"><div class="li"> Работа (количество времени необходимого для выполнения задачи, часы)</div>
</li>
<li class="level3"><div class="li"> Дата начала</div>
</li>
<li class="level3"><div class="li"> Дата окончания</div>
</li>
<li class="level3"><div class="li"> Статус (Не начата | В процессе | Завершена | Отложена)</div>
</li>
<li class="level3"><div class="li"> Исполнитель: выбирается из списка персон; </div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды:</div>
<ul>
<li class="level3"><div class="li"> Сохранить: введенные данные сохраняются в базе; управление передается в форму “Список задач”</div>
</li>
<li class="level3"><div class="li"> Отмена: управление передается в форму “Список задач”</div>
</li>
</ul>
</li>
</ul>
</li>
<li class="level1"><div class="li"> Форма ввода персоны (исполнителя)</div>
<ul>
<li class="level2"><div class="li"> Поля:</div>
<ul>
<li class="level3"><div class="li"> Идентификатор</div>
</li>
<li class="level3"><div class="li"> Фамилия</div>
</li>
<li class="level3"><div class="li"> Имя</div>
</li>
<li class="level3"><div class="li"> Отчество</div>
</li>
<li class="level3"><div class="li"> Должность</div>
</li>
</ul>
</li>
<li class="level2"><div class="li"> Команды:</div>
<ul>
<li class="level3"><div class="li"> Сохранить: введенные данные сохраняются в базе; управление передается в форму “Список персон”</div>
</li>
<li class="level3"><div class="li"> Отмена: управление передается в форму “Список персон”</div>
</li>
</ul>
</li>
</ul>
</li>
</ol>

<p>

Замечание по терминологии: “команда” обозначает любой элемент управления, используемый для запуска операции, к примеру, это может быть кнопка, пиктограмма, гиперссылка и т.п.
</p>

<h2><a name="дополнительные_требования" id="дополнительные_требования">Дополнительные требования</a></h2>
<div class="level2">

<p>

Дополнительные требования реализуются по специальному указанию.
Они включают следующие дополнения к исходной постановке задачи:
</p>
<ul>
<li class="level1"><div class="li"> Поддержка возможности назначения нескольких исполнителей на задачу: </div>
<ul>
<li class="level2"><div class="li"> на форме ввода задачи можно выбрать несколько исполнителей из списка </div>
</li>
</ul>
</li>
</ul>

</div>
</asp:Content>
