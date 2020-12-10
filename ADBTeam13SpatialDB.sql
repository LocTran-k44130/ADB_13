CREATE DATABASE ADBTeam13SpatialDB
GO

USE ADBTeam13SpatialDB
GO

IF OBJECT_ID ( 'dbo.ThuaDat', 'U' ) IS NOT NULL   
    DROP TABLE dbo.ThuaDat;  
GO  

CREATE TABLE [ThuaDat] (
id int IDENTITY (1,1),
Mau nvarchar(10),
MaMD nvarchar(5),
YNghia nvarchar(50),
MatDoNuoc float,
MatDoDoanhThu float,
Location geometry, 
)
GO

INSERT INTO [ThuaDat] (Mau, MaMD, YNghia, MatDoNuoc, MatDoDoanhThu, Location)VALUES(
'Xám', 'M7', 'Trồng lúa', 75000, 250.6,
geometry::STGeomFromText('POLYGON ((-2 -2, 1 -1, 1 3, -1 3, -2 2, -2 -2),(3 0, 5 -2, 7 -2, 7 3, 5 3, 6 1, 3 0))', 0)
)
GO
INSERT INTO [ThuaDat] (Mau, MaMD, YNghia, MatDoNuoc, MatDoDoanhThu, Location)VALUES(
'Nâu', 'M8', 'Trồng cây ăn quả', 45000, 350.4,
geometry::STGeomFromText('POLYGON ((-2 -2, 3 -2, 3 0, 1 0, 1 -1, -2 -2))', 0)
)
GO
INSERT INTO [ThuaDat] (Mau, MaMD, YNghia, MatDoNuoc, MatDoDoanhThu, Location)VALUES(
'Tím', 'M9', 'Trồng rau', 35000, 450.5,
geometry::STGeomFromText('POLYGON ((1 0, 3 0, 5 1, 5 3, 3 4, 1 3, 1 0))', 0)
)
GO

SELECT * FROM ThuaDat