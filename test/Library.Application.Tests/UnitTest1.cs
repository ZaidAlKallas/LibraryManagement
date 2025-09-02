using FluentAssertions;

namespace Library.Application.Tests;

public class DummyTests
{
    [Fact]
    public void Sanity_Check() => true.Should().BeTrue();
}