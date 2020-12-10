USE ADBTeam13SpatialDB
GO

-- 2.a --
SELECT id, mau, MaMD, Location.MakeValid().STArea()*100/10000 AS [DienTich(ha)], MatDoNuoc*(Location.MakeValid().STArea()*100)/10000 AS [LuongNuocCanThiet]
FROM ThuaDat
GO

-- 2.b
SELECT TOP 1 (Location.MakeValid().STArea()*100/10000) AS [MaxArea(ha)], Location
FROM ThuaDat
ORDER BY Location.MakeValid().STArea() DESC

-- 2.c
DECLARE @DienTichBiMat TABLE (id int IDENTITY (1,1), Location geometry) 
DECLARE @kn geometry 
SET @kn = geometry::STGeomFromText('POLYGON ((1 3, 2 -2, 3 -2, 4 -1, 3 4, 1 3))', 0);


DECLARE @m7C geometry
SET @m7C = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M7')
DECLARE @m8C geometry
SET @m8C = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M8')
DECLARE @m9C geometry
SET @m9C = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M9')

INSERT INTO @DienTichBiMat (Location) VALUES(
 (@kn.MakeValid().STIntersection(@m7C))
)
INSERT INTO @DienTichBiMat (Location) VALUES(
 (@kn.MakeValid().STIntersection(@m8C))
)
INSERT INTO @DienTichBiMat (Location) VALUES(
 (@kn.MakeValid().STIntersection(@m9C))
)

SELECT id, Location.MakeValid().STArea()*100/10000 as [DienTichBiMat(ha)], Location FROM @DienTichBiMat

-- 2.d
DECLARE @KenhNuoc geometry 
SET @KenhNuoc = geometry::STGeomFromText('POLYGON ((1 3, 2 -2, 3 -2, 4 -1, 3 4, 1 3))', 0);
DECLARE @m7D geometry
SET @m7D = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M7')
DECLARE @m8D geometry
SET @m8D = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M8')
DECLARE @m9D geometry
SET @m9D = (SELECT Location.MakeValid() FROM ThuaDat where MaMD = 'M9')

UPDATE ThuaDat
SET Location = @m7D.STDifference(@KenhNuoc)
WHERE MaMD = 'M7'

UPDATE ThuaDat
SET Location = @m8D.STDifference(@KenhNuoc)
WHERE MaMD = 'M8'

UPDATE ThuaDat
SET Location = @m9D.STDifference(@KenhNuoc)
WHERE MaMD = 'M9'

SELECT * FROM ThuaDat
