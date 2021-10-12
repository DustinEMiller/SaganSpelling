using System.Collections.Generic;

public class HighFrequencyWord
{
    public string word { get; set; }
    public string soundFile { get; set; }
}

public class OtherWord
{
    public string word { get; set; }
    public string soundFile { get; set; }
}

public class Word
{
    public List<HighFrequencyWord> highFrequencyWords { get; set; }
    public List<OtherWord> otherWords { get; set; }
}

public class WordList
{
    public List<Word> words { get; set; }
}


