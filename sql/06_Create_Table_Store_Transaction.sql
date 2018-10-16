use store;
create table store_transaction (ID int NOT NULL AUTO_INCREMENT,name Varchar(50), type int, transaction_data blob, shippingfee int, discount float, lastupdate long,PRIMARY KEY (ID))