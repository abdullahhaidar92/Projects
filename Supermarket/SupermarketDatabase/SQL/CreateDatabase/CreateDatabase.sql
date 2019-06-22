DECLARE  @dataPath  nvarchar(256),
		@dbName    nvarchar(256),
		@createDB  nvarchar(MAX) 

SET @dataPath = 'C:\databases\SupermarketDB'
SET @dbName = 'Supermarket4'
SET @createDB ='CREATE DATABASE '+''+@dbName+'
				ON ( 	NAME = '+ @dbName+''+'_dat, 
						FILENAME = '''+@dataPath+'\'+@dbName+'.mdf'')
				LOG ON
					( 	NAME = '+ @dbName+''+'_log,
						FILENAME = '''+@dataPath+'\'+@dbName+'.ldf'');'

print @createDB
exec(@createDB);
