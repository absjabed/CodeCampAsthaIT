using Shouldly;

namespace CalculatorService.Tests;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldReturnSum()
    {
        // Arrange
        var calculator = new Calculator();
 
        // Act
        var result = calculator.Add(5, 3);
 
        // Assert
        result.ShouldBe(8);
    }
 
    [Fact]
    public void Subtract_ShouldReturnDifference()
    {
        // Arrange
        var calculator = new Calculator();
 
        // Act
        var result = calculator.Subtract(5, 3);
 
        // Assert
        result.ShouldBe(2);
    }
 
    [Fact]
    public void Multiply_ShouldReturnProduct()
    {
        // Arrange
        var calculator = new Calculator();
 
        // Act
        var result = calculator.Multiply(5, 3);
 
        // Assert
        result.ShouldBe(15);
    }
 
    [Fact]
    public void Divide_ShouldReturnQuotient()
    {
        // Arrange
        var calculator = new Calculator();
 
        // Act
        var result = calculator.Divide(6, 3);
 
        // Assert
        result.ShouldBe(2);
    }
 
    [Fact]
    public void Divide_ByZero_ShouldThrowException()
    {
        // Arrange
        var calculator = new Calculator();
 
        // Act & Assert
        Should.Throw<ArgumentException>(() => calculator.Divide(5, 0))
            .Message.ShouldBe("Cannot divide by zero.");
    }
}