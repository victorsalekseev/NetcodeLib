POP3+Attachments+индикатор загрузки
Class Pop3
Методы
Connect - подключиться USER PASS
Disconnect - отключиться 
GetMsgCountSize - получить количество писем и размер
GetListMsgSize - список писем с размерами
GetAllMessage - получить все письма сразу
GetMessage - Получить письмо
GetMsg - получить объект письмо
MsgOpen - открыть письмо 
MsgGetText - получить письмо в тексте
MsgDelete - удалить письмо

Свойства
error - возвращает ошибку
messages - список писем
msgCount - количество писем

Есть возможность прерывать загрузку.

Class Pop3Msg
Свойства
n, size, from, to, subject, id, date, attachments

Class Pop3Att
Методы
SaveAs

Свойства
filename



