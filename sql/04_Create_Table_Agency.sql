use store;

create table agency (ID int NOT NULL AUTO_INCREMENT, 					
                    name Varchar(50), 
                    type int,
                    lastupdate long,PRIMARY KEY (ID));
                    
                    

SELECT * FROM agency where type = 0 order by lastupdate;