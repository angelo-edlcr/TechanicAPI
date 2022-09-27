/* Database */
CREATE DATABASE Techanic;

/* Products table */
CREATE TABLE Products(
	id int IDENTITY PRIMARY KEY not null,
	code varchar(10) not null,
	name varchar(150) not null,
	price decimal not null,
	stock int not null
);

/* Users table */
CREATE TABLE [User](
	id int IDENTITY PRIMARY KEY not null,
	name varchar(100) not null,
	lastname varchar(100) not null,
	email varchar(150) not null,
	isAdmin bit not null
);

/* Orders table */
CREATE TABLE [Order](
	id int IDENTITY PRIMARY KEY not null,
	customerId int not null,
	productId int not null,
	quantity int not null,
	total decimal not null
);