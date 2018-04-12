DELETE FROM dbo.AspNetUserRoles;
INSERT INTO dbo.AspNetUserRoles(Id, Name)
VALUES (1, 'Admin'), (2, 'Manager'), (3, 'Staff'), (4, 'Tenant'), (5, 'Applicant');

DELETE FROM dbo.Appliances;
INSERT INTO dbo.Appliances(ID, Name, Description, BelongsToAsset_ID)
VALUES ('26188f2f-da0c-45c6-ba67-44795e1d10cd', 'App1', 'desc1', null),
('0c43ef1f-be9b-4bb8-8625-b1498af6f49c', 'App2', 'desc2', null),
('50a24d12-736a-431a-a872-a25d162d6308', 'App3', 'desc3', '29489f9b-dfe1-4225-9928-c1e2bc71eadf');