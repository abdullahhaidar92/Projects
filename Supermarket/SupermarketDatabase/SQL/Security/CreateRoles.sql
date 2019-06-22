/*
alter role Cashier drop member Csh01
alter role Cashier drop member Csh02
drop role Cashier
alter role StockManager drop member Stk01
drop role StockManager
*/
Create Role Cashier 
GRANT EXECUTE ON OBJECT::dbo.AddOrder TO Cashier
GRANT EXECUTE ON OBJECT::dbo.AddItemToOrder TO Cashier
GRANT SELECT ON "ORDER" to Cashier 
GRANT SELECT ON ORDER_ITEMS to Cashier 
GRANT SELECT ON ORDERSView to Cashier 
GRANT SELECT on PRODUCT to Cashier
Grant SELECT,INSERT,Update ON CUSTOMER to Cashier


Create Role StockManager 
GRANT SELECT,Insert,Update on PRODUCT to  StockManager
GRANT SELECT,Insert,Update on STOCK to  StockManager
GRANT SELECT,Insert,Update on SUPPLIER to  StockManager
GRANT SELECT,Insert,Update on SUPPLIER_PRODUCT to  StockManager 
GRANT EXECUTE ON OBJECT::dbo.AddProduct TO StockManager