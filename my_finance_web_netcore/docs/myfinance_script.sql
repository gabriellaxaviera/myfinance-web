CREATE DATABASE myfinance

use myfinance

create table planoconta(
id int identity(1,1) not null, 
descricao varchar(50) not null,
tipo char(1) not null,
primary key (id)
);

create table transacao(
id int identity(1,1) not null,
data datetime not null,
valor decimal (9,2),
historico text,
planocontaid int not null,
primary key(id),
foreign key(planocontaid) references planoconta(id)
);

select * from planoconta

select * from transacao

Select id, data, valor, historico, planocontaid from transacao

insert into planoconta(descricao, tipo)
values('Dividendo de A��es', 'R')

insert into transacao(data, valor, historico, planocontaid)
values('20230205 20:47', 25,'americanas',5)

-- todas as transa��es de despesa no mes de janeiro

select t.data, t.valor, p.descricao
from transacao t
inner join planoconta p on t.planocontaid = p.id
where p.tipo = 'D' and t.data >='20230101' and t.data <='20230131'


--todas as transa��es maiores que 200 reias

select * 
from transacao t
where t.valor > 200

--somatorio de transa��es de receiras e despesas de todo o periodo
select 
	d.total_despesas,
	r.total_receita
from
	(select SUM(valor) as total_despesas 
	from transacao t join planoconta p on p.id = t.planocontaid 
	where p.tipo = 'D') as d,

	(select SUM(valor) as total_receita
	from transacao t join planoconta p on p.id = t.planocontaid 
	where p.tipo = 'D') as r

--media de transsa��es por mes 

	select 
	AVG(valor) as media,
	MONTH(data) as mes 
	from transacao
	group by month(data)

--trasa��es e seus itens de plano de contas
	select t.id, t.data, t.valor, p.descricao, p.tipo
	from transacao t join planoconta p
	on t.planocontaid = p.id


--SQL 

--DQL - Data QWuery Language (SELECT)
--DML - Data Manipulation Language (INSERT, UPDATE, DELETE)
--DDL - Data Definition Language (CREATE, ALTER, DROP)
--CDL - Data Create Language (GRANT, REVOKE)
--DTL - Data Transaction Language (BEGIN, COMMIT, ROLLBACK)