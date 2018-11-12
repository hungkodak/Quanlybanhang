use store;

create table warehouse (ID int NOT NULL AUTO_INCREMENT, 			
                    productId Varchar(4),
                    size varchar(4),
                    quantity int,
                    lastupdate long,PRIMARY KEY (ID));