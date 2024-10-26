use db_cinema;

-- criação de tabelas

-- desabilita chegagem de FK
SET foreign_key_checks = 0;

-- remove a tabela Sala_Filme
DROP TABLE IF EXISTS Sala_Filme;

DROP TABLE IF EXISTS Filme;

DROP TABLE IF EXISTS Sala;

SET foreign_key_checks = 1;

-- Criar tabela Sala
CREATE TABLE Sala (
    IdSala INT AUTO_INCREMENT PRIMARY KEY, 
    NumeroSala INT NOT NULL,               
    Descricao VARCHAR(100) NOT NULL
);

-- Criar tabela Filme
CREATE TABLE Filme (
    IdFilme INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Diretor VARCHAR(100),
    Duracao INT -- Duração em minutos
);

-- Criar tabela de relacionamento entre Sala e Filme 
CREATE TABLE Sala_Filme (
    IdSala INT,
    IdFilme INT,
    PRIMARY KEY (IdSala, IdFilme),
    FOREIGN KEY (IdSala) REFERENCES Sala(IdSala) ON DELETE CASCADE,
    FOREIGN KEY (IdFilme) REFERENCES Filme(IdFilme) ON DELETE CASCADE
);
