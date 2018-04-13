

insert into Appli (ID, Name,Type,AskingRent,Address_ID) values ('6c2aaabe-331e-40d6-b5f1-fefe506bb293','Asset 1','House',2000,1);

insert into Tenants (ID,PhoneNumber,Email,Details,RequestedAssets_ID) values ('bac85386-3111-4931-b38a-c813875bd3c5','6043101010','omegalul@gmail.com','test','4d6ec45a-f68f-4a5e-9c54-cd4c5f30a984');

SET IDENTITY_INSERT Occupancies ON;
insert into Occupancies(ID,StartDate,EndDate,AssetID_ID,ClientID_ID) values (10,SYSDATETIME(),SYSDATETIME(),'6c2aaabe-331e-40d6-b5f1-fefe506bb293','4d6ec45a-f68f-4a5e-9c54-cd4c5f30a984');
SET IDENTITY_INSERT Occupancies OFF;

