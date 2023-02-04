# As an alternative, run the tests with coverage and produce a coverage report
rm -r DotnetStarter.Logic.Tests/TestResults && \
  dotnet test --no-restore --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./TestResults/coverage.cobertura.xml' && \
  reportgenerator "-reports:DotnetStarter.Logic.Tests/TestResults/*.xml" "-targetdir:report" "-reporttypes:Html;lcov" "-title:DotnetStarter"
open report/index.html

#dotnet test --no-restore --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./TestResults/coverage.cobertura.xml'

#coverlet .\DotnetStarter.Logic.Tests\bin\Debug\net7.0\DotnetStarter.Logic.Tests.dll --target "dotnet" --targetargs "test --no-build"

#dotnet watch --project ./DotnetStarter.Logic.Tests test