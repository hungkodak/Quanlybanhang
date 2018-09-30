SELECT * FROM usermanagement.accounts;

SELECT * FROM usermanagement.accounts where username = 'hungkodak' and password = MD5('hungkodak') limit 1;

INSERT INTO accounts(name,username,password,role,lastupdate) VALUES ('Tào Hùng','hungkodak',MD5('hungkodak'),0,date(now()));

DELETE FROM `usermanagement`.`accounts` WHERE (`username` = 'hungkodak');

UPDATE `usermanagement`.`accounts` SET `name` = 'Truong Lap Hung' WHERE (`username` = 'hungkodak');
