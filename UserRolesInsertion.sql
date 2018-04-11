DELETE FROM dbo.AspNetRoles;
INSERT INTO dbo.AspNetRoles(Id, Name)
VALUES (1, 'Admin'), (2, 'Manager'), (3, 'Staff'), (4, 'Tenant'), (5, 'Applicant');