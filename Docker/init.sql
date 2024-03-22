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
   tipoRegistro varchar(2) NOT NULL,
   CONSTRAINT ponto_pkey PRIMARY KEY (id),
   CONSTRAINT ponto_funcionarios_fk FOREIGN KEY (funcionario_id) REFERENCES public.funcionarios(id) 
);

CREATE TABLE public.perfis ( 
   id int NOT NULL,    
   nome varchar(20) NOT NULL,
   CONSTRAINT perfil_pkey PRIMARY KEY (id)
);

CREATE TABLE public.usuarios ( 
   id uuid NOT NULL, 
   login varchar(11) NOT NULL,    
   senha varchar(248) NOT NULL,
   perfil_id int NOT NULL,
   ativo boolean NOT NULL,
   CONSTRAINT usuarios_pkey PRIMARY KEY (id),
   CONSTRAINT usuarios_perfis_fk FOREIGN KEY (perfil_id) REFERENCES public.perfis(id) 
);

CREATE TABLE public.lgpd_solicitacoes ( 
   id uuid NOT NULL, 
   nome varchar(100) NOT NULL, 
   telefone varchar(15) NOT NULL, 
   endereco varchar(100) NOT NULL, 
   removerInformacoesPagamento boolean NOT NULL,
   CONSTRAINT lgpd_solicitacoes_pkey PRIMARY KEY (id)
);

INSERT INTO public.ocupacoes (id, nome) VALUES
	('09f6a1c6-2fe3-4276-8014-b9595437e331','Gerente'),
	('09f6a1c6-2fe3-4276-8014-b9595437e332','Supervisor'),
	('09f6a1c6-2fe3-4276-8014-b9595437e333','Vendedor');

INSERT INTO public.funcionarios (id, nome, matricula, cpf, email, ocupacao_id) VALUES 
   ('6b4f3188-4536-4029-8033-3835c7437f31', 'Ana Maria', 'A000001', '28507433057', 'rm350055@fiap.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e331'),
   ('6b4f3188-4536-4029-8033-3835c7437f32', 'Bruno Pereira', 'A000002', '06997172059', 'rm350055@fiap.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e332'),
   ('6b4f3188-4536-4029-8033-3835c7437f33', 'João Almeida', 'A000003', '02231416077', 'rm350055@fiap.com.br', '09f6a1c6-2fe3-4276-8014-b9595437e333');

INSERT INTO public.perfis (id, nome) VALUES
   (1, 'Administrador'),
   (2, 'Colaborador');

INSERT INTO public.usuarios (id, login, senha, perfil_id, ativo) VALUES
   ('6b4f3188-4536-4029-8033-3835c7437f41', '28507433057', 'W3PD19wro3gdmSCWUcWoKOcXJWA=', 1, true),
   ('6b4f3188-4536-4029-8033-3835c7437f42', '06997172059', 'W3PD19wro3gdmSCWUcWoKOcXJWA=', 2, true),
   ('6b4f3188-4536-4029-8033-3835c7437f43', '02231416077', 'W3PD19wro3gdmSCWUcWoKOcXJWA=', 2, true);

INSERT INTO public.pontos (id, funcionario_id, "data", hora, tiporegistro)
VALUES('d6d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-01', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4622da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-01', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4623da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-01', '14:01:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4624da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-01', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4625da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-02', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4626da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-02', '12:04:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4627da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-02', '14:05:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4628da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-02', '18:07:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4629da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-05', '08:01:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4631da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-05', '12:02:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4641da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-05', '14:03:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4651da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-05', '18:04:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4661da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-06', '08:05:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4671da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-06', '12:06:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4681da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-06', '14:07:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4691da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-06', '18:08:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da1003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-07', '08:09:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da2003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-07', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da3003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-07', '14:11:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da4003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-07', '18:26:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da5003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-08', '08:30:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da6003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-08', '12:45:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da7003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-08', '14:51:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da8003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-08', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da9003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-09', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da0103e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-09', '11:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da0203e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-09', '14:01:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da0303e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-09', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da0403e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-12', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da0503e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-12', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da0603e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-12', '14:01:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da0703e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-12', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da0803e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-13', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da0903e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-13', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da0013e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-13', '14:01:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da0023e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-13', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da0033e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-14', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da0043e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-14', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da0053e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-14', '14:01:22', 'E2'),
('a6d9a614-6e4a-470e-abdf-4621da0063e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-14', '18:06:22', 'S2'),

('a6d9a614-6e4a-470e-abdf-4621da0073e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-15', '08:00:22', 'E1'),
('a6d9a614-6e4a-470e-abdf-4621da0083e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-15', '12:05:22', 'S1'),
('a6d9a614-6e4a-470e-abdf-4621da0093e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-15', '14:01:22', 'E2'),
('a6d9a624-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-15', '18:06:22', 'S2'),

('a6d9a634-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-16', '08:00:22', 'E1'),
('a6d9a644-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-16', '12:05:22', 'S1'),
('a6d9a654-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-16', '14:01:22', 'E2'),
('a6d9a664-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-16', '18:06:22', 'S2'),

('a6d9a674-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-19', '08:00:22', 'E1'),
('a6d9a684-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-19', '12:05:22', 'S1'),
('a6d9a694-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-19', '14:01:22', 'E2'),
('06d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-19', '18:06:22', 'S2'),

('16d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-20', '08:00:22', 'E1'),
('26d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-20', '12:05:22', 'S1'),
('36d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-20', '14:01:22', 'E2'),
('46d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-20', '18:06:22', 'S2'),

('56d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-21', '08:00:22', 'E1'),
('66d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-21', '12:05:22', 'S1'),
('76d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-21', '14:01:22', 'E2'),
('86d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-21', '18:06:22', 'S2'),

('96d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-22', '08:00:22', 'E1'),
('a1d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-22', '12:05:22', 'S1'),
('a2d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-22', '14:01:22', 'E2'),
('a3d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-22', '18:06:22', 'S2'),

('a4d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-23', '08:00:22', 'E1'),
('a5d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-23', '12:05:22', 'S1'),
('a7d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-23', '14:01:22', 'E2'),
('a8d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-23', '18:06:22', 'S2'),

('a9d9a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-26', '08:00:22', 'E1'),
('a619a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-26', '12:05:22', 'S1'),
('a629a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-26', '14:01:22', 'E2'),
('a639a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-26', '18:06:22', 'S2'),

('a649a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-27', '08:00:22', 'E1'),
('a659a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-27', '12:05:22', 'S1'),
('a669a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-27', '14:01:22', 'E2'),
('a679a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-27', '18:06:22', 'S2'),

('a689a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-28', '08:00:22', 'E1'),
('a699a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-28', '12:05:22', 'S1'),
('a6d1a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-28', '14:01:22', 'E2'),
('a6d2a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-28', '18:06:22', 'S2'),

('a6d3a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-29', '08:00:22', 'E1'),
('a6d4a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-29', '12:05:22', 'S1'),
('a6d5a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-29', '14:01:22', 'E2'),
('a6d6a614-6e4a-470e-abdf-4621da0003e9'::uuid, '6b4f3188-4536-4029-8033-3835c7437f32'::uuid, '2024-02-29', '18:06:22', 'S2');