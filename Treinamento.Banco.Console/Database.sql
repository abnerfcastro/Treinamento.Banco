CREATE TABLE [dbo].[Usuario]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [Nome] VARCHAR(50) NOT NULL, 
    [Idade] INT NULL, 
    [Email] VARCHAR(100) NOT NULL , 
    [Nickname] VARCHAR(50) NOT NULL, 
    [Cidade] VARCHAR(50) NULL
)

INSERT INTO [Usuario] (Nome, Idade, Email, Nickname, Cidade) VALUES
('Renan Ferreira', 24, 'renanferreira@gmail.com', 'renanf', 'São Paulo'),
('Isabela Cunha', 29, 'isa.cunha@hotmail.com', 'isacunha123', 'Campinas'),
('Rafael Martins Barbosa', 63, 'raf.barbosa@uol.com.br', 'rafa1955', 'Volta Redonda'),
('Thiago Fernandes Azevedo', 31, 'thiazevedo@gmail.com', 'thiazevedo', 'Porto Alegre'),
('Carla Almeida Martins', 22, 'carlalmeida@yahoo.com', 'carlinha123', 'Curitiba'),
('Tânia Fernandes', 25, 'tania.fernandes@gmail.com', 'taniaf', 'Belo Horizonte')

SELECT * FROM Usuario
