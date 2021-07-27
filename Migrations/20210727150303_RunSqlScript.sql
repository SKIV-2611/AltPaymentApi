USE mvc_db;   
GO  
ALTER TABLE dbo.Payment   
ADD CONSTRAINT DboID UNIQUE (DboID);   
GO  