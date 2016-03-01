using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UniversityRegistrar
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_registrar_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StudentsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_StudentEqual_ReturnsTrue()
    {
      //Arrange, Act
      Student firstStudent = new Student("John Smith", new DateTime(2015, 01, 18));
      Student secondStudent = new Student("John Smith", new DateTime(2015, 01, 18));

      //Assert
      Assert.Equal(firstStudent, secondStudent);
    }

    [Fact]
    public void Test_Save_SavesStudentToDatabase()
    {
      //Arrange
      Student testStudent = new Student("John Smith", new DateTime(2015, 01, 18));
      testStudent.Save();

      //Act
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToStudentObject()
    {
      //Arrange
      Student testStudent = new Student("John Smith", new DateTime(2015, 01, 18));
      testStudent.Save();

      //Act
      Student savedStudent = Student.GetAll()[0];

      int result = savedStudent.GetId();
      int testId = testStudent.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
