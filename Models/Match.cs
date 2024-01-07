using System.ComponentModel.DataAnnotations;

namespace SCOREgrp05.Models;

public class Match
{
    public int matchId { get; set; }
    public string? equipe1 { get; set; }
    public string? equipe2 { get; set; }
    public string score { get; set; }
    public int temps { get; set; }
    public ICollection<But>? but { get; set; }
}