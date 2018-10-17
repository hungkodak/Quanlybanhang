use store;
create table store_transaction (ID int VarChar,name Varchar(50), type int, transaction_data blob, shippingfee int, discount float, lastupdate long,PRIMARY KEY (ID))