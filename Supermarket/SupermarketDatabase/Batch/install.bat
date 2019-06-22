@echo off
set project_path=C:\Users\hp\Desktop\AbdullahHaidar_AmiraBaltajy\SupermarketDatabase
set server=localhost
set dbname=Supermarket4
osql  -S %server% -E -i %project_path%\SQL\CreateDatabase\CreateDatabase.sql -o %project_path%\Log\CreatDatabse.log
echo Database %dbname% Created 
osql  -S %server% -d %dbname% -E -i %project_path%\SQL\CreateTables\CreateTables.sql  -o %project_path%\Log\CreateTables.log
echo Tables Created 
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Triggers\CreateForeignTriggers.sql  -o %project_path%\Log\CreateForeignTriggers.log
echo Foreign Keys Created 
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddCustomer.sql  -o %project_path%\Log\AddCustomer.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddEmployee.sql  -o %project_path%\Log\AddEmployee.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddOrder.sql  -o %project_path%\Log\AddOrder.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddProduct.sql  -o %project_path%\Log\AddProduct.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddSupplier.sql  -o %project_path%\Log\AddSupplier.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddStock.sql  -o %project_path%\Log\AddStock.log
osql  -S %server% -d %dbname% -E -i  %project_path%\SQL\Procedures\AddItemToOrder.sql  -o %project_path%\Log\AddItemToOrder.log
echo Procedures Created
osql  -S %server% -d %dbname% -E -i  %project_path%\Data\Insert\AddCustomers.sql  -o %project_path%\Log\AddCustomers.log
osql  -S %server% -d %dbname% -E -i  %project_path%\Data\Insert\ADDEmployees.sql  -o %project_path%\Log\AddEmployees.log
osql  -S %server% -d %dbname% -E -i  %project_path%\Data\Insert\AddSuppliers.sql  -o %project_path%\Log\AddSuppliers.log
osql  -S %server% -d %dbname% -E -i  %project_path%\Data\Insert\AddProducts.sql  -o %project_path%\Log\AddProducts.log
osql  -S %server% -d %dbname% -E -i  %project_path%\Data\Insert\AddStocks.sql  -o %project_path%\Log\AddStocks.log
echo Data Inserted