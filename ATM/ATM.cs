namespace ATM;

public class Atm
{
    private readonly HashSet<int> _availableNotes;

    public Atm(HashSet<int> availableNotes)
    {
        //must be ordered
        _availableNotes = availableNotes;
    }

    public List<int> Withdraw(int amount)
    {
        var withdrawnNotes = new List<int>();

        var list = AttemptToWithdraw(amount, withdrawnNotes, _availableNotes);
        return list ?? new List<int>();
    }

    public List<int>? AttemptToWithdraw(int amount, List<int> currentNotes, HashSet<int> possibleNoteValues)
    {
        // If no possible note values, it has exhausted all possible options. Withdraw is invalid
        if (possibleNoteValues.Count == 0)
        {
            return null;
        }

        var candidateNotes = new List<int>(currentNotes); //to avoid changing the parameter  object
        var nextNote = NextLargestNote(possibleNoteValues);
        var remainingAmount = amount - nextNote;

        //Perfect match, return it
        if (remainingAmount == 0)
        {
            candidateNotes.Add(nextNote);
            return candidateNotes;
        }

        //keep on keeping on
        if (remainingAmount > 0)
        {
            candidateNotes.Add(nextNote);
            var attemptedNotes = AttemptToWithdraw(remainingAmount, candidateNotes, possibleNoteValues);
            //Couldn't withdraw with those possible notes.
            if (attemptedNotes == null)
            {
                //Remove this last added candidate note
                candidateNotes.RemoveAt(candidateNotes.Count - 1);
                //Remove this largest possible note and try with the next set
                var newPossibleNoteValues = new HashSet<int>(possibleNoteValues);
                newPossibleNoteValues.Remove(possibleNoteValues.First());
                return AttemptToWithdraw(amount, candidateNotes, newPossibleNoteValues);
            }

            return attemptedNotes;
        }

        //Remaining amount is smaller than 0. 
        return null;
    }

    private int NextLargestNote(HashSet<int> possibleNotes)
    {
        return possibleNotes.First();
    }
}