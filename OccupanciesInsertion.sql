select * from Occupancies;
SET IDENTITY_INSERT Occupancies ON;
insert into Occupancies(ID,StartDate,EndDate) values (1,SYSDATETIME(),SYSDATETIME());
insert into Occupancies(ID,StartDate,EndDate) values (2,SYSDATETIME(),SYSDATETIME());
insert into Occupancies(ID,StartDate,EndDate) values (3,SYSDATETIME(),SYSDATETIME());
insert into Occupancies(ID,StartDate,EndDate) values (4,SYSDATETIME(),SYSDATETIME());
insert into Occupancies(ID,StartDate,EndDate) values (5,SYSDATETIME(),SYSDATETIME());
SET IDENTITY_INSERT Occupancies OFF;
select * from Occupancies;