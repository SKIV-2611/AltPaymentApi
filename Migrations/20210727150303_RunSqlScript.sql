﻿USE mvc_db;   
GO  
ALTER TABLE dbo.Payments
ADD CONSTRAINT DboID UNIQUE (DboID);   
GO  