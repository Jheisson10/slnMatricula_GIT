create sequence SEQ_ALUMNO
start with 1
increment by 1
maxvalue 99999
minvalue 1;

create sequence SEQ_MATRICULA
start with 1
increment by 1
maxvalue 99999
minvalue 1;

CREATE TABLE "ALUMNO" 
(	
"ID_ALUMNO" NUMBER(*,0), 
"DNI" VARCHAR2(15 BYTE), 
"NOMBRES" VARCHAR2(100 BYTE), 
"APELLIDOS" VARCHAR2(100 BYTE), 
"ESTADO" NUMBER(1,0) DEFAULT 1, 
PRIMARY KEY ("ID_ALUMNO")
);

Insert into ALUMNO (ID_ALUMNO,DNI,NOMBRES,APELLIDOS,ESTADO) values ('1','11111111','Juan','Perez','1');
Insert into ALUMNO (ID_ALUMNO,DNI,NOMBRES,APELLIDOS,ESTADO) values ('2','22222222','Pedro','Rojas','1');
Insert into ALUMNO (ID_ALUMNO,DNI,NOMBRES,APELLIDOS,ESTADO) values ('3','33333333','Ernesto','Segundo','1');
Insert into ALUMNO (ID_ALUMNO,DNI,NOMBRES,APELLIDOS,ESTADO) values ('4','44444444','Jorge','Diaz','1');
Insert into ALUMNO (ID_ALUMNO,DNI,NOMBRES,APELLIDOS,ESTADO) values ('5','55555555','Luis','Gomez','1');


CREATE TABLE "CURSO" 
(	
"ID_CURSO" NUMBER(*,0), 
"NOMBRE" VARCHAR2(50 BYTE), 
"CREDITOS" NUMBER(*,0), 
"ESTADO" NUMBER DEFAULT 1, 
PRIMARY KEY ("ID_CURSO")
);

Insert into CURSO (ID_CURSO,NOMBRE,CREDITOS,ESTADO) values ('1','Curso 1','2','1');
Insert into CURSO (ID_CURSO,NOMBRE,CREDITOS,ESTADO) values ('2','Curso 2','2','1');
Insert into CURSO (ID_CURSO,NOMBRE,CREDITOS,ESTADO) values ('3','Curso 3','3','1');
Insert into CURSO (ID_CURSO,NOMBRE,CREDITOS,ESTADO) values ('4','Curso 4','5','1');
Insert into CURSO (ID_CURSO,NOMBRE,CREDITOS,ESTADO) values ('5','Curso 5','6','1');


CREATE TABLE "MATRICULA" 
(	
"ID_MATRICULA" NUMBER(*,0), 
"ID_ALUMNO" NUMBER(*,0), 
"MODALIDAD" NUMBER(*,0), 
"ID_CURSO" NUMBER(*,0), 
"FECHA_MATRICULA" CHAR(20 BYTE), 
"FECHA_ANULACION" DATE, 
PRIMARY KEY ("ID_MATRICULA")
);

CREATE TABLE "SECCION" 
(	"ID_SECCION" NUMBER(*,0), 
"NOMBRE" VARCHAR2(50 BYTE), 
"ESTADO" NUMBER DEFAULT 1, 
PRIMARY KEY ("ID_SECCION")
);

CREATE TABLE "VACANTE" 
(	
"ID_VACANTE" NUMBER(*,0), 
"ID_CURSO" NUMBER(*,0), 
"ID_SECCION" NUMBER(*,0), 
"CANTIDAD_VACANTE" NUMBER(*,0), 
PRIMARY KEY ("ID_VACANTE")
);

Insert into VACANTE (ID_VACANTE,ID_CURSO,ID_SECCION,CANTIDAD_VACANTE) values ('1','1','1','-2');
Insert into VACANTE (ID_VACANTE,ID_CURSO,ID_SECCION,CANTIDAD_VACANTE) values ('2','2','2','1');
Insert into VACANTE (ID_VACANTE,ID_CURSO,ID_SECCION,CANTIDAD_VACANTE) values ('3','3','2','3');
Insert into VACANTE (ID_VACANTE,ID_CURSO,ID_SECCION,CANTIDAD_VACANTE) values ('4','4','3','2');
Insert into VACANTE (ID_VACANTE,ID_CURSO,ID_SECCION,CANTIDAD_VACANTE) values ('5','5',null,null);

COMMIT;

create or replace PROCEDURE InsertarMatricula (
    id_alumno IN integer,
    id_modalidad IN integer,
    pid_vacante  IN number,
    fechaMatricula IN VARCHAR,    
    p_resultado OUT NUMBER
) AS
BEGIN
    -- Insertar la matrícula en la tabla Matriculas
    INSERT INTO matricula (id_matricula, id_alumno, modalidad, id_curso, fecha_matricula)
    VALUES (SEQ_ALUMNO.NEXTVAL, id_alumno,id_modalidad,pid_vacante, to_char(to_date(fechaMatricula),'DD/MM/YYYY HH24:MI:SS'));

    update vacante 
    set cantidad_vacante = cantidad_vacante - 1
    where id_vacante = pid_vacante;

    -- Obtener el ID de la matrícula recién insertada
    SELECT 1 INTO p_resultado FROM DUAL;

    -- Confirmar que la inserción fue exitosa
    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        -- En caso de error, hacer un rollback
        ROLLBACK;
        p_resultado := -1; -- Puedes establecer un valor de error personalizado aquí
END InsertarMatricula;


