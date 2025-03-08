CREATE DATABASE db_MenuPro

USE db_MenuPro

CREATE TABLE tbl_Usuarios
(
	id_Usuario INT PRIMARY KEY IDENTITY,
	nm_Usuario VARCHAR(60) NOT NULL,
	us_Usuario VARCHAR(20) NOT NULL UNIQUE,
	sn_Usuario VARCHAR(20) NOT NULL
)

INSERT INTO tbl_Usuarios(nm_Usuario, us_Usuario, sn_Usuario)
VALUES('Teste Teste', 'Teste', 'Teste1234')

SELECT * FROM tbl_Usuarios