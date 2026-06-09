CREATE VIEW vw_ClaimBillingOnly AS
SELECT 
    Status,
    BilledAmount,
    ReimbursedAmt,
    (BilledAmount - ReimbursedAmt) AS OutstandingAmount
FROM Claim;