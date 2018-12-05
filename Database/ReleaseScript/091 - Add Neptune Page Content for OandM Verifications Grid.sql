
-- insert a new page type for WQMP O&M Verifications Grid heading
insert into dbo.NeptunePageType
values (31, 'WaterQualityMaintenancePlanOandMVerifications', 'Water Quality Maintenance Plan O&M Verifications', 2)

-- insert page content for WQMP O&M Verifications Grid heading
insert into dbo.NeptunePage
values (2, 31, '<p>The Orange County Stormwater Tools provides an inventory of O&M Verification visits for Water Quality Management Plans (WQMPs). Use the grid below to see all the recorded O&M Verifications. You can click the view button to see additional details.</p>')
