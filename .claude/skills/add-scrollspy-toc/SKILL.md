---
name: add-scrollspy-toc
description: Add a scrollspy Table of Contents sidebar to a page component with signal-based scroll tracking.
allowed-tools: [Read, Glob, Grep, Edit, Write]
argument-hint: <ComponentName>
---

# Add Scrollspy TOC Skill

When the user invokes `/add-scrollspy-toc <ComponentName>`:

## Overview

This skill adds a scrollspy Table of Contents (TOC) sidebar to a page component. The TOC highlights the current section as the user scrolls and allows click-to-scroll navigation.

## Pattern: Inline Signal-Based Scrollspy

All scrollspy implementations should use the **inline signal-based approach** for consistency:

- Angular signals for reactive state (`activeSectionId`, `activeParentSectionId`)
- RxJS `fromEvent(window, "scroll")` with `debounceTime(10)` for scroll tracking
- `getBoundingClientRect().top` to determine active section
- Template bindings with `[class.active]="activeSectionId() === section.id"`

**DO NOT** use the directive-based approach (`ScrollSpySectionDirective`, `ScrollSpyLinkDirective`) for new implementations.

## Implementation Steps

### Step 1: Add Imports to Component

```typescript
import { Component, OnInit, OnDestroy, signal } from "@angular/core";
import { fromEvent, Subscription, debounceTime } from "rxjs";
```

### Step 2: Add Properties

For **flat sections** (no hierarchy):
```typescript
private scrollSubscription: Subscription | null = null;

// Define your sections
sections = [
    { id: "section-1", title: "First Section" },
    { id: "section-2", title: "Second Section" },
    { id: "section-3", title: "Third Section" },
];

activeSectionId = signal<string | null>(null);
```

For **hierarchical sections** (parent/child):
```typescript
private scrollSubscription: Subscription | null = null;
private sectionIds: string[] = [];
private sectionToParentMap = new Map<string, string>();

activeSectionId = signal<string | null>(null);
activeParentSectionId = signal<string | null>(null);
```

### Step 3: Implement Lifecycle Hooks

```typescript
ngOnInit(): void {
    // For static sections:
    this.setupScrollSpy();

    // For dynamic sections (from API), call after data loads:
    // this.buildSectionMap(data);
    // setTimeout(() => this.setupScrollSpy(), 150);
}

ngOnDestroy(): void {
    this.scrollSubscription?.unsubscribe();
}
```

### Step 4: Add Scrollspy Methods

For **flat sections**:
```typescript
private setupScrollSpy(): void {
    this.scrollSubscription?.unsubscribe();

    this.scrollSubscription = fromEvent(window, "scroll")
        .pipe(debounceTime(10))
        .subscribe(() => this.updateActiveSection());

    this.updateActiveSection();
}

private updateActiveSection(): void {
    const offset = 120; // Adjust based on header height
    let activeId: string | null = null;

    for (const section of this.sections) {
        const el = document.getElementById(section.id);
        if (el) {
            const rect = el.getBoundingClientRect();
            if (rect.top <= offset) {
                activeId = section.id;
            }
        }
    }

    if (activeId !== null && activeId !== this.activeSectionId()) {
        this.activeSectionId.set(activeId);
    }
}

scrollToSection(sectionId: string): void {
    const element = document.getElementById(sectionId);
    if (element) {
        element.scrollIntoView({ behavior: "smooth", block: "start" });
        this.activeSectionId.set(sectionId);
    }
}
```

For **hierarchical sections**:
```typescript
private buildSectionMap(sections: YourSectionType[]): void {
    this.sectionToParentMap.clear();
    this.sectionIds = [];

    sections.forEach((parent) => {
        const parentId = `section-${parent.ID}`;
        this.sectionIds.push(parentId);
        this.sectionToParentMap.set(parentId, parentId);

        if (parent.ChildSections) {
            parent.ChildSections.forEach((child) => {
                const childId = `section-${child.ID}`;
                this.sectionIds.push(childId);
                this.sectionToParentMap.set(childId, parentId);
            });
        }
    });
}

private setupScrollSpy(): void {
    this.scrollSubscription?.unsubscribe();

    this.scrollSubscription = fromEvent(window, "scroll")
        .pipe(debounceTime(10))
        .subscribe(() => this.updateActiveSection());

    this.updateActiveSection();
}

private updateActiveSection(): void {
    const offset = 120;
    let activeId: string | null = null;

    for (const id of this.sectionIds) {
        const el = document.getElementById(id);
        if (el) {
            const rect = el.getBoundingClientRect();
            if (rect.top <= offset) {
                activeId = id;
            }
        }
    }

    if (activeId !== null && activeId !== this.activeSectionId()) {
        this.activeSectionId.set(activeId);
        this.activeParentSectionId.set(this.sectionToParentMap.get(activeId) ?? activeId);
    }
}

scrollToSection(sectionId: string, parentId?: string): void {
    const element = document.getElementById(sectionId);
    if (element) {
        element.scrollIntoView({ behavior: "smooth", block: "start" });
        this.activeSectionId.set(sectionId);
        this.activeParentSectionId.set(parentId ?? sectionId);
    }
}

isSectionExpanded(section: YourSectionType): boolean {
    return this.activeParentSectionId() === `section-${section.ID}`;
}
```

### Step 5: Add HTML Template

**Layout structure:**
```html
<div class="page-container">
    <div class="dashboard-layout">
        <!-- TOC Sidebar -->
        <div class="toc-sidebar">
            <div class="toc-title">Contents</div>
            <nav class="toc-nav">
                @for (section of sections; track section.id) {
                <div class="toc-item">
                    <a href="javascript:void(0)"
                       [class.active]="activeSectionId() === section.id"
                       (click)="scrollToSection(section.id)">
                        {{ section.title }}
                    </a>
                </div>
                }
            </nav>
        </div>

        <!-- Main Content -->
        <div class="content-area">
            @for (section of sections; track section.id) {
            <section [id]="section.id" class="content-section">
                <h2>{{ section.title }}</h2>
                <!-- Section content -->
            </section>
            }
        </div>
    </div>
</div>
```

### Step 6: Add SCSS Styles

```scss
@use "/src/scss/abstracts" as *;

.dashboard-layout {
    display: grid;
    grid-template-columns: 250px 1fr;
    gap: 1.5rem;
    align-items: start;

    @media (max-width: 992px) {
        grid-template-columns: 1fr;
    }
}

.toc-sidebar {
    position: sticky;
    top: 1rem;

    @media (max-width: 992px) {
        position: static;
    }

    .toc-title {
        font-weight: bold;
        margin-bottom: 0.5rem;
        padding-left: 1rem;
    }

    .toc-nav {
        .toc-item {
            margin-bottom: 0.25rem;

            > a {
                display: block;
                padding: 0.25rem 0.5rem 0.25rem 1rem;
                color: var(--bs-gray-700);
                text-decoration: none;
                border-left: 2px solid transparent;
                transition: all 0.2s;

                &:hover {
                    color: var(--btn-primary-bg-color, var(--bs-primary));
                    border-left-color: var(--btn-primary-bg-color, var(--bs-primary));
                }

                &.active {
                    color: var(--btn-primary-bg-color, var(--bs-primary));
                    font-weight: bold;
                    border-left-color: var(--btn-primary-bg-color, var(--bs-primary));
                }
            }

            .toc-children {
                margin-left: 0.5rem;

                .toc-child {
                    display: block;
                    padding: 0.25rem 0.5rem 0.25rem 1rem;
                    font-size: 0.875rem;
                    color: var(--bs-gray-600);
                    text-decoration: none;
                    border-left: 2px solid transparent;
                    transition: all 0.2s;

                    &:hover,
                    &.active {
                        color: var(--btn-primary-bg-color, var(--bs-primary));
                        border-left-color: var(--btn-primary-bg-color, var(--bs-primary));
                    }

                    &.active {
                        font-weight: bold;
                    }
                }
            }
        }
    }
}

.content-area {
    .content-section {
        margin-bottom: 1.5rem;
        scroll-margin-top: 1rem;

        h2 {
            margin: 0 0 0.5rem;
            font-size: 1.5rem;
        }
    }
}
```

## Checklist

- [ ] Component implements `OnInit` and `OnDestroy`
- [ ] Signals used for `activeSectionId` (and `activeParentSectionId` if hierarchical)
- [ ] `fromEvent(window, "scroll")` with `debounceTime(10)`
- [ ] Subscription cleaned up in `ngOnDestroy`
- [ ] Section IDs match between TOC links and content sections
- [ ] `scroll-margin-top` set on content sections
- [ ] TOC sidebar has `position: sticky` with appropriate `top` value
- [ ] Mobile responsive (grid collapses to single column)
- [ ] Active state styling applied correctly
