use db_cinema;

-- Script de inserção

-- tabela Sala
INSERT INTO Sala (NumeroSala, Descricao) VALUES
(1, 'Sala 1'),
(2, 'Sala 2'),
(3, 'Sala 3');

-- tabela Filme
INSERT INTO Filme (Nome, Diretor, Duracao) VALUES
('Aventuras no Espaço', 'Carlos Silva', 120),
('O Grande Showman', 'Ana Souza', 105),
('Os Incríveis', 'Fernando Oliveira', 115),
('Coração Valente', 'Juliana Costa', 178),
('A Origem', 'Roberto Mendes', 148),
('Parasita', 'Mariana Lima', 132),
('O Senhor dos Anéis: A Sociedade do Anel', 'Ricardo Almeida', 178),
('Vingadores: Ultimato', 'Patrícia Martins', 181);

-- tabela Sala_Filme
INSERT INTO Sala_Filme (IdSala, IdFilme) VALUES
(1, 1), 
(1, 2), 
(2, 3), 
(2, 4), 
(3, 5), 
(3, 6), 
(1, 7), 
(2, 8); 
