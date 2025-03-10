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

CREATE TABLE tbl_Produtos
(
    id_Produto INT PRIMARY KEY IDENTITY,
    nm_Produto VARCHAR(60) NOT NULL UNIQUE,
    vlr_Produto DECIMAL(10, 2) NOT NULL
)

SELECT * FROM tbl_Produtos

INSERT INTO tbl_Produtos(nm_Produto, vlr_Produto)
VALUES('maça', 4.50)


CREATE TABLE tbl_LucroDia
(
    id_Produto INT PRIMARY KEY IDENTITY,
    valorProduto DECIMAL (10, 2) NOT NULL,
	dataValor DATETIME NOT NULL
)

SELECT * FROM tbl_LucroDia