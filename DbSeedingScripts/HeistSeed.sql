declare @StatusName NVARCHAR(MAX)
SET @StatusName = 'AVAILABLE';
If not exists (select ID from MemberStatus where Name=@StatusName) BEGIN INSERT MemberStatus(ID, Name)Values (NEWID(), @StatusName) END
SET @StatusName = 'EXPIRED';
If not exists (select ID from MemberStatus where Name=@StatusName) BEGIN INSERT MemberStatus(ID, Name)Values (NEWID(), @StatusName) END
SET @StatusName = 'INCARCERATED';
If not exists (select ID from MemberStatus where Name=@StatusName) BEGIN INSERT MemberStatus(ID, Name)Values (NEWID(), @StatusName) END
SET @StatusName = 'RETIRED';
If not exists (select ID from MemberStatus where Name=@StatusName) BEGIN INSERT MemberStatus(ID, Name)Values (NEWID(), @StatusName) END


declare @Value NVARCHAR(MAX)

SET @Value = '*';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END

SET @Value = '**';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(),@Value) END
SET @Value = '***';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END

SET @Value = '****';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END

SET @Value = '*****';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END
SET @Value = '******';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END
SET @Value = '******';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END

SET @Value = '*******';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END
SET @Value = '********';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END
SET @Value = '*********';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END
SET @Value = '**********';
If not exists (select ID from Levels where Value=@Value) BEGIN INSERT Levels(ID, Value)Values (NEWID(), @Value) END


declare @Gender NVARCHAR(MAX)
SET @Gender = 'M';
If not exists (select ID from Sex where Name=@Gender) BEGIN INSERT Sex(ID, Name)Values (NEWID(), @Gender) END
SET @Gender = 'F';
If not exists (select ID from Sex where Name=@Gender) BEGIN INSERT Sex(ID, Name)Values (NEWID(), @Gender) END


