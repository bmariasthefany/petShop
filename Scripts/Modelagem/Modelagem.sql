
BEGIN TRANSACTION

	CREATE TABLE TipoProduto (
		CodTipoProduto SMALLINT
			CONSTRAINT TipoProduto_PK PRIMARY KEY IDENTITY(1, 1),
		Valor MONEY,
		Tipo VARCHAR(30)
	)

	CREATE TABLE Marca (
		CodMarca SMALLINT 
			CONSTRAINT Marca_PK PRIMARY KEY IDENTITY(1, 1),
		Nome VARCHAR(20) NOT NULL
	)

	CREATE TABLE Produto (
		CodProduto SMALLINT 
			CONSTRAINT Produto_PK PRIMARY KEY IDENTITY(1, 1),
		CodTipoProduto SMALLINT
			CONSTRAINT TipoProduto_Produto_FK1
				FOREIGN KEY REFERENCES TipoProduto (CodTipoProduto),
		CodMarca SMALLINT
			CONSTRAINT Marca_Produto_FK1
				FOREIGN KEY REFERENCES Marca (CodMarca)
	)

	CREATE TABLE TipoDado (
		CodTipoDado SMALLINT
			CONSTRAINT TipoDado_PK PRIMARY KEY IDENTITY(1, 1),
		TipoDado VARCHAR(15)
	)

	CREATE TABLE Genero (
		CodGenero SMALLINT
			CONSTRAINT Genero_PK PRIMARY KEY IDENTITY(1,1),
		Genero VARCHAR(1),
		CodTipoDado SMALLINT 
			CONSTRAINT TipoDado_Genero_FK1
				FOREIGN KEY REFERENCES TipoDado(CodTipoDado)
	)

	CREATE TABLE Email (
		CodEmail SMALLINT
			CONSTRAINT Email_PK PRIMARY KEY IDENTITY(1, 1),
		Email VARCHAR(50),
		CodTipoDado SMALLINT 
			CONSTRAINT TipoDado_Email_FK1
				FOREIGN KEY REFERENCES TipoDado(CodTipoDado)
	)

	CREATE TABLE Telefone (
		CodTelefone SMALLINT 
			CONSTRAINT Telefone_PK PRIMARY KEY IDENTITY(1, 1),
		Telefone VARCHAR(15),
		CodTipoDado SMALLINT 
			CONSTRAINT TipoDado_Telefone_FK1
				FOREIGN KEY REFERENCES TipoDado(CodTipoDado)
	)

	CREATE TABLE Endereco (
		CodEndereco SMALLINT
			CONSTRAINT Endereco_PK PRIMARY KEY IDENTITY (1, 1),
		CEP VARCHAR(9),
		NomeRua VARCHAR(20),
		NumCasa VARCHAR(6)
	)
	CREATE TABLE Cliente (
		CodCliente SMALLINT
			CONSTRAINT Cliente_PK PRIMARY KEY IDENTITY(1, 1),
		Nome VARCHAR(20) NOT NULL,
		DataNascimento VARCHAR(20) NOT NULL,
		CPF VARCHAR(11) NOT NULL,
		CodGenero SMALLINT
			CONSTRAINT Genero_Cliente_FK1
				FOREIGN KEY REFERENCES Genero(CodGenero),
		CodEmail SMALLINT
			CONSTRAINT Email_Cliente_FK1
				FOREIGN KEY REFERENCES Email(CodEmail),
		CodTelefone SMALLINT
			CONSTRAINT Telefone_Cliente_FK1
				FOREIGN KEY REFERENCES Telefone(CodTelefone),
		CodEndereco SMALLINT
			CONSTRAINT Endeco_Cliente_FK1
				FOREIGN KEY REFERENCES Endereco(CodEndereco),
	)
	
	CREATE TABLE ClienteEmail (
		CodCliente SMALLINT
			CONSTRAINT Cliente_ClienteEmail_FK1
				FOREIGN KEY REFERENCES Cliente(CodCliente),
		CodEmail SMALLINT
			CONSTRAINT Email_ClienteEmail_FK1
				FOREIGN KEY REFERENCES Email(CodEmail)
	)

	CREATE TABLE Animal (
		CodAnimal SMALLINT 
			CONSTRAINT Animal_PK PRIMARY KEY IDENTITY(1, 1),
		Nome VARCHAR(20) NOT NULL,
		Raça VARCHAR(20) NOT NULL,
		DataNascimento DATE,
		Tipo VARCHAR(10) NOT NULL,
		CodGenero SMALLINT
			CONSTRAINT Genero_Animal_FK1
				FOREIGN KEY REFERENCES Genero(CodGenero)
	)

	CREATE TABLE Loja (
		CodLoja SMALLINT
			CONSTRAINT Loja_PK PRIMARY KEY IDENTITY(1, 1),
		CPNJ VARCHAR(14) NOT NULL,
		CodTelefone SMALLINT
			CONSTRAINT Telefone_Loja_FK1
				FOREIGN KEY REFERENCES Telefone(CodTelefone),
		CodEmail SMALLINT
			CONSTRAINT Email_Loja_FK1
				FOREIGN KEY REFERENCES Email(CodEmail),
		CodEndereco SMALLINT
			CONSTRAINT Endeco_Loja_FK1
				FOREIGN KEY REFERENCES Endereco(CodEndereco),
	)

	CREATE TABLE LojaEmail (
		CodLoja SMALLINT
			CONSTRAINT Loja_LojaEmail_FK1
				FOREIGN KEY REFERENCES Loja(CodLoja),
		CodEmail SMALLINT
			CONSTRAINT Email_LojaEmail_FK1
				FOREIGN KEY REFERENCES Email(CodEmail)
	)

	CREATE TABLE Funcionario (
		CodFuncionario SMALLINT
			CONSTRAINT Funcionario_PK PRIMARY KEY IDENTITY(1, 1),
		Nome VARCHAR(20) NOT NULL,
		DataNascimento VARCHAR(20) NOT NULL,
		CPF VARCHAR(11) NOT NULL,
		CodGenero SMALLINT
			CONSTRAINT Genero_Funcionario_FK1
				FOREIGN KEY REFERENCES Genero(CodGenero),
		CodEmail SMALLINT
			CONSTRAINT Email_Funcionario_FK1
				FOREIGN KEY REFERENCES Email(CodEmail),
		CodTelefone SMALLINT
			CONSTRAINT Telefone_Funcionario_FK1
				FOREIGN KEY REFERENCES Telefone(CodTelefone),
		CodEndereco SMALLINT
			CONSTRAINT Endeco_Funcionario_FK1
				FOREIGN KEY REFERENCES Endereco(CodEndereco),

	)

	CREATE TABLE FuncionarioEmail (
	CodFuncionario SMALLINT
			CONSTRAINT Funcionario_FuncionarioEmail_FK1
				FOREIGN KEY REFERENCES Funcionario(CodFuncionario),
	CodEmail SMALLINT
			CONSTRAINT Email_FuncionarioEmail_FK1
				FOREIGN KEY REFERENCES Email(CodEmail)
	)

	CREATE TABLE Servico (
		CodServico SMALLINT
			CONSTRAINT Servico_PK PRIMARY KEY IDENTITY(1, 1),
		CodFuncionario SMALLINT
			CONSTRAINT Funcionario_Servico_FK1
				FOREIGN KEY REFERENCES Funcionario(CodFuncionario),
		CodAnimal SMALLINT
			CONSTRAINT Animal_Servico_FK1
				FOREIGN KEY REFERENCES Animal(CodAnimal)
	)	


	CREATE TABLE Venda (
		CodVenda SMALLINT
			CONSTRAINT Venda_PK PRIMARY KEY IDENTITY(1, 1),
		Data DATE,
		Valor MONEY,
		CodLoja SMALLINT
			CONSTRAINT Loja_Venda_FK1
				FOREIGN KEY REFERENCES Loja(CodLoja),
		CodProduto SMALLINT
			CONSTRAINT Produto_Venda_FK1
				FOREIGN KEY REFERENCES Produto(CodProduto),
		CodCliente SMALLINT
			CONSTRAINT Cliente_Venda_FK1
				FOREIGN KEY REFERENCES Cliente(CodCliente),
		CodFuncionario SMALLINT
			CONSTRAINT Funcionario_Venda_FK1
				FOREIGN KEY REFERENCES Funcionario(CodFuncionario)
	)

	CREATE TABLE VendaServico (
		CodVenda SMALLINT
			CONSTRAINT Venda_VendaServico_FK1
				FOREIGN KEY REFERENCES Venda(CodVenda),
		CodServico SMALLINT
			CONSTRAINT Servico_VendaServico_FK1
				FOREIGN KEY REFERENCES Servico(CodServico)
	)

	CREATE TABLE VendaProduto (
		Quantidade SMALLINT,
		CodVenda SMALLINT
			CONSTRAINT Venda_VendaProduto_FK1
				FOREIGN KEY REFERENCES Venda(CodVenda),
		CodProduto SMALLINT
			CONSTRAINT Produto_VendaProduto_FK1
				FOREIGN KEY REFERENCES Produto(CodProduto)
	)

--COMMIT TRANSACTION
--ROLLBACK

