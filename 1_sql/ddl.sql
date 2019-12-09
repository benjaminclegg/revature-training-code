use master;
go

-- create
create database PizzaBox;
go

use PizzaBox;
go

create schema [Order];
go

create schema Accounts;
go

create table [Order].[Order]
(
    OrderId int not null identity(1,2) -- primary key
    ,UserId int not null -- foreign key
    ,StoreId int not null --foreign key
    ,TotalCost decimal(3,2) not null
    ,OrderDate datetime2(0) not null -- checked
    ,Active bit not null
);

create table [Order].[OrderPizza]
(
    OrderPizzaId int not null identity(1,2) --primary key
    ,OrderId int not null -- foreign key
    ,PizzaId int not null -- foreign key
)

create table [Order].Pizza
(
    PizzaId int not null identity(1,2) -- primary key
    ,SizeId int not null -- foreign key
    ,CrustId int not null -- foreign key
    ,Price decimal(2,2) not null
    ,Active bit not null
)

-- ALTER
alter table [Order].[Order]
    add constraint PK_Order_OrderId primary key (OrderId);

alter table [Order].[OrderPizza]
    add constraint PK_OrderPizza_OrderPizzaId primary key (OrderPizzaId);

alter table [Order].[Pizza]
    add constraint PK_Order_PizzaId primary key (PizzaId);

alter table [Order].[OrderPizzaId]
    add constraint FK_OrderPizza_OrderId foreign key (OrderId) references [Order].[Order](OrderId);

alter table [Order].[OrderPizzaId]
    add constraint FK_OrderPizza_PizzaId foreign key (PizzaId) references [Order].[Pizza](PizzaId);

alter table [Order].[Order]
    add constraint DF_Order_Active default (1) for Active;

alter table [Order].[Pizza]
    add constraint DF_Pizza_Active default (1) for Active;

alter table [Order].[Order]
    add constraint CK_Order_OrderDate check (OrderDate > '2019-11-11');

alter table [Order].[Order]
    drop constraint CK_Order_OrderDate;

alter table [Order].[Order]
    add TipAmount decimal(2,2) null;

alter table [Order].[Order]
    drop column TipAmount;

-- drop

drop table [Order].[OrderPizza];

-- truncate
truncate table [Order].[OrderPizza];