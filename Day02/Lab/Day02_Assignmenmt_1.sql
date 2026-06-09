WITH EncounterCounts AS (
    SELECT
        p.ProviderId,
        p.FullName,
        d.Name AS DepartmentName,
        COUNT(e.EncounterId) AS TotalEncounters
    FROM Provider p
    INNER JOIN Department d
        ON p.DepartmentId = d.DepartmentId
    INNER JOIN Encounter e
        ON p.ProviderId = e.ProviderId
    GROUP BY
        p.ProviderId,
        p.FullName,
        d.Name
)
SELECT
    FullName,
    DepartmentName,
    TotalEncounters,
    RANK() OVER (ORDER BY TotalEncounters DESC) AS ProviderRank
FROM EncounterCounts
ORDER BY TotalEncounters DESC;