CREATE FUNCTION [dbo].[Split] (@DelimitedString nvarchar(max), @Delimiter nvarchar(10))
RETURNS table
WITH SCHEMABINDING
AS
RETURN (
    WITH Pieces (ID, start, stop) AS (
      SELECT CAST(1 AS bigint), CAST(1 AS bigint), CAST(CHARINDEX(@Delimiter, @DelimitedString) AS bigint)
      UNION ALL
      SELECT ID + 1, CAST(stop + 1 As bigint), CAST(CHARINDEX(@Delimiter, @DelimitedString, stop + 1) AS bigint)
      FROM Pieces
      WHERE stop > 0
    )
    SELECT ID,
      SUBSTRING(@DelimitedString, start, CASE WHEN stop > 0 THEN stop-start ELSE LEN(@DelimitedString) END) AS Element
    FROM Pieces
  )

GO