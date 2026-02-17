// Parses cobertura XML and outputs per-class coverage with uncovered line numbers.
// Usage: node parse-coverage.js <cobertura.xml> [classFilter1,classFilter2,...]
//
// Output format (one JSON object per line for easy reading):
//   { class, file, lineCoverage, branchCoverage, uncoveredLines }

const fs = require("fs");

const xmlPath = process.argv[2];
const classFilters = process.argv[3] ? process.argv[3].split(",").map((f) => f.trim().toLowerCase()) : null;

if (!xmlPath) {
    console.error("Usage: node parse-coverage.js <cobertura.xml> [classFilter1,classFilter2,...]");
    process.exit(1);
}

const xml = fs.readFileSync(xmlPath, "utf-8");

// Split into class blocks
const classBlocks = xml.split(/<class /);
classBlocks.shift(); // remove preamble before first <class

for (const block of classBlocks) {
    const nameMatch = /name="([^"]*)"/.exec(block);
    const fileMatch = /filename="([^"]*)"/.exec(block);
    const lineRateMatch = /line-rate="([^"]*)"/.exec(block);
    const branchRateMatch = /branch-rate="([^"]*)"/.exec(block);

    if (!nameMatch) continue;

    const className = nameMatch[1];
    const fileName = fileMatch ? fileMatch[1] : null;
    const lineRate = lineRateMatch ? (parseFloat(lineRateMatch[1]) * 100).toFixed(1) : "0.0";
    const branchRate = branchRateMatch ? (parseFloat(branchRateMatch[1]) * 100).toFixed(1) : "0.0";

    // Apply class filter
    if (classFilters) {
        const lowerName = className.toLowerCase();
        const matches = classFilters.some((f) => lowerName.includes(f));
        if (!matches) continue;
    } else {
        // Without filter, skip 0% classes
        if (lineRate === "0.0" && branchRate === "0.0") continue;
    }

    // Skip compiler-generated async state machine classes — their coverage
    // is already reflected in the parent method's lines.
    if (className.includes("/<") && className.includes(">d__")) continue;

    // Extract uncovered lines (hits="0")
    const uncoveredLines = [];
    const linePattern = /<line number="(\d+)" hits="(\d+)"/g;
    let m;
    while ((m = linePattern.exec(block)) !== null) {
        if (m[2] === "0") {
            uncoveredLines.push(parseInt(m[1], 10));
        }
    }

    // Collapse consecutive line numbers into ranges for readability
    const ranges = [];
    let i = 0;
    while (i < uncoveredLines.length) {
        const start = uncoveredLines[i];
        let end = start;
        while (i + 1 < uncoveredLines.length && uncoveredLines[i + 1] === end + 1) {
            end = uncoveredLines[++i];
        }
        ranges.push(start === end ? `${start}` : `${start}-${end}`);
        i++;
    }

    console.log(
        JSON.stringify({
            class: className,
            file: fileName,
            lineCoverage: `${lineRate}%`,
            branchCoverage: `${branchRate}%`,
            uncoveredLines: ranges.length > 0 ? ranges.join(", ") : "(none)",
        })
    );
}
