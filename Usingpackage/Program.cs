using Finance;

double p = double.Parse(args[0]);
int m = 10;
for (int n = 1; n < m; ++n)
{
    float r = args[1]  switch
    {
        "EducationLoan" => new EducationLoan().Common(p, n),
        "HomeLoan" => new HomeLoan().Common(p,n),
        "PersonalLoan" => new PersonalLoan().Common(p, n),
        _ => throw new ArgumentException("Invalid Argument!")
    };
    double emi = Loans.GetMonthlyInstallment(p, n, r);
    Console.WriteLine("{0, -6}{1, 16:0.00}", n, emi);
}

