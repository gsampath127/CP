CREATE FUNCTION [dbo].[fnHashBytesNVARCHARMAX]  
(  
 @Algorithm VARCHAR(10),  
 @Text NVARCHAR(MAX)  
)  
RETURNS VARBINARY(8000)  
WITH SCHEMABINDING  
AS  
BEGIN  
 DECLARE @NumHash INT;  
 DECLARE @HASH VARBINARY(8000);  
 SET @NumHash = CEILING(DATALENGTH(@Text) / (8000.0));  
 /* HashBytes only supports 8000 bytes so split the string if it is larger */  
 WHILE @NumHash > 1  
 BEGIN  
  -- # * 4000 character strings  
  WITH a AS  
  (SELECT 1 AS n UNION ALL SELECT 1), -- 2   
  b AS  
  (SELECT 1 AS n FROM a, a a1),       -- 4  
  c AS  
  (SELECT 1 AS n FROM b, b b1),       -- 16  
  d AS  
  (SELECT 1 AS n FROM c, c c1),       -- 256  
  e AS  
  (SELECT 1 AS n FROM d, d d1),       -- 65,536  
  f AS  
  (SELECT 1 AS n FROM e, e e1),       -- 4,294,967,296 = 17+ TRILLION characters  
  factored AS  
  (SELECT ROW_NUMBER() OVER (ORDER BY n) rn FROM f),  
  factors AS  
  (SELECT rn, (rn * 4000) + 1 factor FROM factored)  
  SELECT @Text = CAST  
   (  
    (  
     SELECT CONVERT(VARCHAR(MAX), HASHBYTES(@Algorithm, SUBSTRING(@Text, factor - 4000, 4000)), 1)  
     FROM factors  
     WHERE rn <= @NumHash  
     FOR XML PATH('')  
    ) AS NVARCHAR(MAX)  
   );  
    
  SET @NumHash = CEILING(DATALENGTH(@Text) / (8000.0));  
 END;  
 SET @HASH = CONVERT(VARBINARY(8000), HASHBYTES(@Algorithm, @Text));  
 RETURN @HASH;  
END;  
GO

