CREATE DATABASE agendarTareas

CREATE TABLE TAREA (
IdTarea INT IDENTITY (1,1),
Titulo VARCHAR(50) NOT NULL,
Descripcion VARCHAR(100),
DiaSemana INT NOT NULL,
HoraInicio TIME NOT NULL,
HoraFin TIME NOT NULL,
TareaCompletada BIT NOT NULL
)

SELECT Titulo, Descripcion, DiaSemana, HoraInicio, HoraFin, TareaCompletada FROM TAREA ORDER BY IdTarea DESC

INSERT INTO TAREA
(
    Titulo,
    Descripcion,
    DiaSemana,
    HoraInicio,
    HoraFin,
    TareaCompletada
)
VALUES
('Enviar reporte', 'Reporte semanal', 5, '15:00', '16:00', 0),
('Ejercicio', 'Rutina diaria', 2, '06:30', '07:30', 1);

INSERT INTO TAREA (Titulo, Descripcion, DiaSemana, HoraInicio, HoraFin, TareaCompletada)
VALUES
('Desayunar', 'Desayuno diario', 1, '07:00', '07:30', 1),
('Leer documentación', 'Leer documentación del proyecto', 2, '09:00', '10:00', 0),
('Práctica de código', 'Practicar CRUD en ASP.NET', 3, '16:00', '18:00', 0),
('Salir a caminar', 'Caminar 30 minutos', 6, '17:00', '17:30', 1),
('Planificar semana', 'Organizar tareas de la semana', 7, '20:00', '21:00', 0);
