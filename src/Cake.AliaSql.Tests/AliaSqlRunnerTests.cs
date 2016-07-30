using System;
using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using Xunit;

namespace Cake.AliaSql.Tests
{
    public sealed class AliaSqlRunnerTests
    {
        public sealed class TheRunMethod
        {

            [Fact]
            public void Should_Throw_If_AliaSql_Runner_Was_Not_Found()
            {
                // Given
                var fixture = new AliaSqlFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("AliaSql: Could not locate executable.", result.Message);
            }

            [Theory]
            [InlineData("C:/AliaSql/AliaSql.exe", "C:/AliaSql/AliaSql.exe")]
            [InlineData("./tools/AliaSQL/tools/AliaSQL.exe", "/Working/tools/AliaSQL/tools/AliaSQL.exe")]
            public void Should_Use_AliaSql_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new AliaSqlFixture {Settings = {ToolPath = toolPath}};
                fixture.GivenSettingsToolPathExist();

                // When
                fixture.Run();

                // Then
                fixture.ProcessRunner.Received(1).Start(Arg.Is<FilePath>(
                    fp => fp.FullPath == expected),
                    Arg.Any<ProcessSettings>());
            }

            [Fact]
            public void Should_Find_AliaSql_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new AliaSqlFixture();

                // When
                fixture.Run();

                // Then
                fixture.ProcessRunner.Received(1).Start(Arg.Is<FilePath>(
                    fp => fp.FullPath == "/Working/tools/AliaSQL.exe"),
                    Arg.Any<ProcessSettings>());
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new AliaSqlFixture();

                // When
                fixture.Run();

                // Then
                fixture.ProcessRunner.Received(1).Start(
                    Arg.Any<FilePath>(),
                    Arg.Is<ProcessSettings>(ps => ps.WorkingDirectory.FullPath == "/Working"));
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new AliaSqlFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.Equal("AliaSql: Process was not started.", result.Message);
                Assert.IsType<CakeException>(result);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new AliaSqlFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.Equal("AliaSql: Process returned an error (exit code 1).", result.Message);
                Assert.IsType<CakeException>(result);
            }

            [Fact]
            public void Should_Throw_If_No_Arguments_Folder()
            {
                // Given
                var fixture = new AliaSqlFixture {Settings = new AliaSqlSettings()};
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentException>(result);
            }
        }
    }
}
