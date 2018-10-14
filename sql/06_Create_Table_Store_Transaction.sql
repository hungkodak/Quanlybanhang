use store;
create table store_transaction (ID int NOT NULL AUTO_INCREMENT,name Varchar(50), type int, transaction_data blob, lastupdate Datetime,PRIMARY KEY (ID))