create table Account
(
    ID integer not null AUTO_INCREMENT primary key,
    Login varchar(50) not null,
    Pass varchar(20) not null,
    Is_blocked boolean not null default false
);
create  table Human
(
    ID integer not null auto_increment primary key,
    First_name varchar(20) not null,
    Last_name varchar(100) not null,
    Date_of_birth date not null,
    Sex integer not null,
    e_mail varchar(50) not null,
    tel varchar(10) not null,
    zip_code varchar(5) not null,
    ID_account integer not null  unique
);
create table Sex
(
  ID integer not null auto_increment primary key,
  name varchar(20) not null,
  short_name char(1)  null
);
create table Role
(
   ID integer not null auto_increment primary key,
   name varchar(20) not null
);
create table Account_Role
(
   ID integer not null auto_increment primary key,
   ID_account integer not null,
   ID_role integer not null
);
create table TelephoneBook
(
   ID integer not null auto_increment primary key,
   name varchar(20) not null,
   surname varchar(20) not null,
   TelephoneNumber varchar(16) not null
);
insert TelephoneBook (name, surname, TelephoneNumber)
values ('Sergey', 'Prisyzhnykh','+79566854514');
