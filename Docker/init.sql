CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public.ocupacoes ( 
   id uuid NOT NULL, 
   nome varchar(80) NOT NULL, 
   CONSTRAINT ocupacoes_pkey PRIMARY KEY (id) 
);

CREATE TABLE public.funcionarios ( 
   id uuid NOT NULL, 
   nome varchar(100) NOT NULL, 
   cpf varchar(11) NULL, 
   email varchar(80) NULL, 
   matricula varchar(15) NOT NULL, 
   ocupacao_id uuid NOT NULL, 
   CONSTRAINT funcionarios_pkey PRIMARY KEY (id), 
   CONSTRAINT funcionarios_ocupacoes_fk FOREIGN KEY (ocupacao_id) REFERENCES public.ocupacoes(id) 
);

CREATE TABLE public.pontos ( 
   id uuid NOT NULL, 
   funcionario_id uuid NOT NULL,
   data date NOT NULL,   
   hora time NOT NULL,
   tipoRegistro varchar(1) NOT NULL,
   CONSTRAINT ponto_pkey PRIMARY KEY (id),
   CONSTRAINT ponto_funcionarios_fk FOREIGN KEY (funcionario_id) REFERENCES public.funcionarios(id) 
);

CREATE TABLE public.lgpd_solicitacoes ( 
   id uuid NOT NULL, 
   nome varchar(100) NOT NULL, 
   telefone varchar(15) NOT NULL, 
   endereco varchar(100) NOT NULL, 
   removerInformacoesPagamento boolean NOT NULL,
   CONSTRAINT lgpd_solicitacoes_pkey PRIMARY KEY (id)
);

INSERT INTO public.ocupacoes (id,nome) VALUES
	('09f6a1c6-2fe3-4276-8014-b9595437e331','Gerente'),
	('09f6a1c6-2fe3-4276-8014-b9595437e332','Supervisor'),
	('09f6a1c6-2fe3-4276-8014-b9595437e333','Vendedor');

INSERT INTO public.funcionarios (id, nome, matricula, cpf, email, ocupacao_id) VALUES 
   ('6b4f3188-4536-4029-8033-3835c7437f31', 'Ana Maria', 'A000001', '28507433057', 'ana@empresa.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e331'),
   ('6b4f3188-4536-4029-8033-3835c7437f32', 'Bruno Pereira', 'A000002', '06997172059', 'bruno@empresa.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e332'),
   ('6b4f3188-4536-4029-8033-3835c7437f33', 'João Almeida', 'A000003', '02231416077', 'joao@empresa.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e333');

