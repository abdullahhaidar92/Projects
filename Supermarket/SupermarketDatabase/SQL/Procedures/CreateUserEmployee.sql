--drop Procedure CreateUserEmployee
Create Procedure CreateUserEmployee    @EID   char(5),
									 @PASSWORD nvarchar(10)                  
As
Begin
declare @sql char(200)
select @sql='create login '+@EID+' with password = '''+@PASSWORD+''' ,Default_Database = SuperMarket ';
exec(@sql);
select @sql='create user '+@EID+' for login '+@EID;
exec(@sql);
set @sql='create view ViewEmployeeInfo'+@EID+' as select * from Employee where EID='''+@EID+''' ' 
exec(@sql);
set @sql='Grant Select on ViewEmployeeInfo'+@EID+' to '+@EID 
exec(@sql);
End



