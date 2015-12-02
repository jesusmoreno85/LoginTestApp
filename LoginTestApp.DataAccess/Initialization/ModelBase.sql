ALTER TABLE dbo.[ModelBaseTableName]
  ADD DEFAULT (USER_NAME()) FOR [CreatedBy] 

ALTER TABLE dbo.[ModelBaseTableName]
  ADD DEFAULT (GETUTCDATE()) FOR [CreatedDate] 
