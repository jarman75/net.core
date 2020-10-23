DECLARE @RC int
DECLARE @CupsCode nvarchar(25) = 'ES0021000001643784DV'
DECLARE @FechaInicio datetime = '01-10-2019'
DECLARE @FechaFin datetime = '30-10-2019'

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[usp_DatosCalculoElectrico] 
   @CupsCode
  ,@FechaInicio
  ,@FechaFin
GO