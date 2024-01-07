using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SCOREgrp05.Models;

public class But
{
    public int butId { get; set; }


    [ForeignKey("Match")]
    public int matchId { get; set; }
    public Match? match { get; set; }


    public string score { get; set; }
    public int temps { get; set; }
    public string joueur { get; set; }
}