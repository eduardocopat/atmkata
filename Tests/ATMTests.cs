using System.Collections.Generic;
using ATM;
using Xunit;

namespace Tests;

public class AtmTests
{
    [Fact]
    public void Withdraw_Exact_Notes()
    {
        var availableNotes = new HashSet<int> { 50 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(50);

        Assert.Equivalent(new List<int> { 50 }, notes);
    }

    [Fact]
    public void Withdraw_Duplicate_Amount_Of_Notes()
    {
        var availableNotes = new HashSet<int> { 50 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(100);

        Assert.Equivalent(new List<int> { 50, 50 }, notes);
    }

    [Fact]
    public void Withdraw_Combination_of_Notes()
    {
        var availableNotes = new HashSet<int> { 50, 20 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(70);

        Assert.Equivalent(new List<int> { 50, 20 }, notes);
    }
    
    [Fact]
    public void Withdraw_Combination_of_Notes_But_Not_The_Highest()
    {
        var availableNotes = new HashSet<int> { 50, 20 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(110);

        Assert.Equivalent(new List<int> { 50, 20, 20, 20 }, notes);
    }

    [Fact]
    public void Withdraw_impossible_value()
    {
        var availableNotes = new HashSet<int> { 50, 20 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(55);

        Assert.Equivalent(new List<int> { }, notes);
    }
    
    [Fact]
    public void Withdraw_impossible_value_smaller()
    {
        var availableNotes = new HashSet<int> { 50, 20 };
        var atm = new Atm(availableNotes);
        var notes = atm.Withdraw(10);

        Assert.Equivalent(new List<int> { }, notes);
    }
}