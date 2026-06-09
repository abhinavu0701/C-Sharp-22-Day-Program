CREATE PROCEDURE sp_ExecutiveDashboard
AS
BEGIN

    
    SELECT 
        COUNT(*) AS TotalActivePatients
    FROM Patient
    WHERE IsActive = 1;


    SELECT TOP 5
        Departmentid,
        COUNT(*) AS TotalEncounters
    FROM Encounter
    GROUP BY DepartmentId
    ORDER BY TotalEncounters DESC;


    SELECT 
        COUNT(*) AS DeniedClaims
    FROM Claim
    WHERE Status = 'Denied';

END;