
-- Back out incorrect conversion (back to sq ft) then apply correct conversion to acres
Update dbo.Parcel set ParcelAreaInAcres = ParcelAreainAcres / 0.0002471053821147119 * 0.00002295682